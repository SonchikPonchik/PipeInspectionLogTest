using Core;
using Domain.Interfaces;
using Infrastructure.Business;
using Infrastructure.Data;
using PipeInspectionLog.Models;
using PipeInspectionLog.Models.Business;
using PipeInspectionLog.Models.VM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PipeInspectionLog.Controllers
{
    public class HomeController : Controller
    {
        //Доступ к БД
        private UnitOfWork _unitOfWork;

        public HomeController()
        {
            _unitOfWork = new UnitOfWork();
        }

        //Get метод главной страницы журнала 
        [HttpGet]
        public ActionResult Index()
        {
            DateTime now = DateTime.Now.Date;
            List<PipeLog> data = GetPipeLogByDate(now);

            return View(data);
        }

        private List<PipeLog> GetPipeLogByDate(DateTime date)
        {
            return _unitOfWork.PipeLogRepository.Get(x => DbFunctions.TruncateTime(x.InspectionDate) == date,
                null, "Diameter, Nomenclature").ToList();
        }

        //Post метод главной страницы журнала 
        [HttpPost]
        public ActionResult Index(DateTime? InspectionDate)
        {
            DateTime date =  CheckDate(InspectionDate);
            List<PipeLog> data = GetPipeLogByDate(date);

            return View(data);
        }

        private DateTime CheckDate(DateTime? inspectionDate)
        {
            if (inspectionDate == null)
                return DateTime.Now.Date;
            DateTime date = (DateTime)inspectionDate;
            return date.Date;
        }

        //Get страница создания новой записи
        [HttpGet]
        public ActionResult Create()
        {
            CreateViewModel createVM = new CreateViewModel();
            createVM = (CreateViewModel)GetSelectLists(createVM);
            return View(createVM);
        }

        private AbstractPipeLogVM GetSelectLists(AbstractPipeLogVM vm)
        {
            vm.Diameters = new SelectList(_unitOfWork.DiameterRepository.Get(),
                "DiameterId", "Value");
            vm.Nomenclatures = new SelectList(_unitOfWork.NomenclatureRepository.Get(),
                "NomenclatureId", "NomenclatureId");
            return vm;
        }

        //Post страница создания новой записи
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel createVM)
        {
            if (ModelState.IsValid)
            {
                PipeLog pipeLog = new PipeLog
                {
                    NomenclatureId = createVM.NomenclatureId,
                    DiameterId = createVM.DiameterId,
                    EndExternalDiameter1 = createVM.EndExternalDiameter1,
                    EndExternalDiameter2 = createVM.EndExternalDiameter2,
                    CenterExternalDiameter = createVM.CenterExternalDiameter,
                    PipeNumber = createVM.PipeNumber,
                    InspectionDate = GetNow(),
                    Note = createVM.Note
                };
                pipeLog.MaxDiameterDeviation = GetMaxDiameterDeviation(pipeLog);

                AddNewPipeLogItem(pipeLog);

                return RedirectToAction("Index");
            }
            createVM = (CreateViewModel)GetSelectLists(createVM);
            return View(createVM);
        }

        //метод добавления в создаваемую запись PipeLog максимального отклонения диаметра 
        private double GetMaxDiameterDeviation(PipeLog item)
        {
            double defaultDiameter = GetDefaultDiameter(item.DiameterId).Value;
            MaxDiameterDeviation deviation = new MaxDiameterDeviation(defaultDiameter, item.EndExternalDiameter1, item.EndExternalDiameter2, item.CenterExternalDiameter);

            return deviation.maxDeviation;
        }

        //метод получения целевого диаметра
        private Diameter GetDefaultDiameter(int diameterId)
        {
            return _unitOfWork.DiameterRepository.GetById(diameterId);
        }

        //Метод получения даты 
        private DateTime GetNow()
        {
            return DateTime.Now;
        }

        //Добавление новой записи в журнал (PipeLog)
        private void AddNewPipeLogItem(PipeLog pipeLog)
        {
            _unitOfWork.PipeLogRepository.Insert(pipeLog);
            SaveChanges();
        }

        //сохранение изменений в БД
        private void SaveChanges()
        {
            _unitOfWork.Save();
        }

        //Get страница изменения записи журнала 
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PipeLog pipeLog = _unitOfWork.PipeLogRepository.GetById(id);
            if (pipeLog == null)
            {
                return HttpNotFound();
            }

            UpdateViewModel editVM = new UpdateViewModel
            {
                PipeLogId = (int)id,
                PipeNumber = pipeLog.PipeNumber,
                NomenclatureId = pipeLog.NomenclatureId,
                DiameterId = pipeLog.DiameterId,
                EndExternalDiameter1 = pipeLog.EndExternalDiameter1,
                EndExternalDiameter2 = pipeLog.EndExternalDiameter2,
                CenterExternalDiameter = pipeLog.CenterExternalDiameter,
                Note = pipeLog.Note
            };

            editVM = (UpdateViewModel)GetSelectLists(editVM);

            return View(editVM);
        }

        //Post страница изменения записи журнала
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UpdateViewModel editVM)
        {
            if (ModelState.IsValid)
            {
                PipeLog pipeLog = new PipeLog
                {
                    PipeLogId = editVM.PipeLogId,
                    NomenclatureId = editVM.NomenclatureId,
                    DiameterId = editVM.DiameterId,
                    EndExternalDiameter1 = editVM.EndExternalDiameter1,
                    EndExternalDiameter2 = editVM.EndExternalDiameter2,
                    CenterExternalDiameter = editVM.CenterExternalDiameter,
                    PipeNumber = editVM.PipeNumber,
                    InspectionDate = GetNow(),
                    Note = editVM.Note
                };

                pipeLog.MaxDiameterDeviation = GetMaxDiameterDeviation(pipeLog);


                EditPipeLogItem(pipeLog);
                return RedirectToAction("Index");
            }

            editVM = (UpdateViewModel)GetSelectLists(editVM);
            return View(editVM);
        }

        //изменение записи журнала в БД
        private void EditPipeLogItem(PipeLog pipeLog)
        {
            _unitOfWork.PipeLogRepository.Update(pipeLog);
            SaveChanges();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PipeLog pipeLog = _unitOfWork.PipeLogRepository.GetById(id);
            if (pipeLog == null)
            {
                return HttpNotFound();
            }

            DeletePipeLogItem(pipeLog.PipeLogId);

            return RedirectToAction("Index");
        }

        //метод удаления из бд
        private void DeletePipeLogItem(int pipeLogId)
        {
            _unitOfWork.PipeLogRepository.Delete(pipeLogId);
            SaveChanges();
        }
    }
}