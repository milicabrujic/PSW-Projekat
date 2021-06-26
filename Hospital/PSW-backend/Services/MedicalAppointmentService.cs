using PSW_backend.Adapters;
using PSW_backend.Dtos;
using PSW_backend.Models;
using PSW_backend.Repositories;
using PSW_backend.Repositories.Interfaces;
using PSW_backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Services
{
    public class MedicalAppointmentService : IMedicalAppointmentService
    {
        private readonly IMedicalAppointmentRepository _medicalAppointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        public MedicalAppointmentService(IMedicalAppointmentRepository medicalAppointmentRepository, IDoctorRepository doctorRepository)
        {
            this._medicalAppointmentRepository = medicalAppointmentRepository;
            this._doctorRepository = doctorRepository;
        }
        public MedicalAppointmentDto SaveAppointment(MedicalAppointmentDto medicalAppointmentDto)
        {
            MedicalAppointment medicalAppointment = MedicalAppointmentAdapter.MedicalAppointmentDtoToMedicalsAppointment(medicalAppointmentDto);
           return MedicalAppointmentAdapter.MedicalAppointmentToMedicalAppointmentDto(_medicalAppointmentRepository.SaveMedicalAppointment(medicalAppointment));
        }
        public MedicalAppointmentDto EndMedicalAppointment(int id)
        {
            return MedicalAppointmentAdapter.MedicalAppointmentToMedicalAppointmentDto(_medicalAppointmentRepository.EndMedicalAppointment(id));
        }
        public MedicalAppointmentDto CancelMedicalAppointment(int id)
        {           
            return  MedicalAppointmentAdapter.MedicalAppointmentToMedicalAppointmentDto(_medicalAppointmentRepository.CancelMedicalAppointment(id));
        }
        public MedicalAppointmentDto FindAppointment(MedicalAppointmentDto medicalAppointmentDto, string priority)
        {
            Doctor doctor = _doctorRepository.FindById(medicalAppointmentDto.DoctorId);
            DateTime dateTime = medicalAppointmentDto.Date;
            if (priority.Equals("time") && doctor.Type == Enums.DoctorType.General) {             
                DateTime doctorFreeTime = FindDoctorTime(doctor, dateTime);
                if (!dateTime.Equals(doctorFreeTime)) {                
                    return null;
                }
            }
            else if (priority.Equals("time") && doctor.Type == Enums.DoctorType.Specialist) {             
                doctor = FindDoctorSpecialist(doctor,dateTime);               
                if (doctor == null) {                 
                    return null;
                }
                else {
                    medicalAppointmentDto.DoctorId = doctor.Id;
                }                               
            }
            else {             
                dateTime = FindDoctorTime(doctor, dateTime);
                medicalAppointmentDto.Date = dateTime;
            }
            return medicalAppointmentDto;
        }
        public List<MedicalAppointment> GetDoctorActiveAppointments(int id)
        {
            return _medicalAppointmentRepository.GetDoctorAppointments(id);
        }

        #region heplper_function

        public DateTime FindDoctorTime(Doctor doctor, DateTime dateTime)
        {    
            bool changeTime = false;
            if (CheckDateEquality(doctor,dateTime)) {
                dateTime = dateTime.AddDays(1);
                changeTime = true;
            }                           
            if (changeTime)
            {
                FindDoctorTime(doctor, dateTime);
            }
            return dateTime;
        }

        public Doctor FindDoctorSpecialist(Doctor specialist,DateTime dateTime)
        {
            Doctor doctor = specialist;
            if (CheckDateEquality(specialist, dateTime)) {
                doctor = ChangeDoctor(dateTime);
            }                  
            return doctor;
        }
        public Doctor ChangeDoctor(DateTime dateTime)
        {
            List<Doctor> specialists = _doctorRepository.DoctorSpecialists();
            foreach (Doctor specialist in specialists)
            {
                bool iSNewSpecialist = false;
                if (CheckDateEquality(specialist, dateTime)) {
                    iSNewSpecialist = true;
                }                            
                if (iSNewSpecialist == false) { 
                    return specialist;
                }
            }            
            return null;
        }
        public bool CheckDateEquality(Doctor doctor, DateTime dateTime) {
            List<MedicalAppointment> appointments = GetDoctorActiveAppointments(doctor.Id);
            foreach (MedicalAppointment appointment in appointments)
            {
                if (appointment.Date.Equals(dateTime))
                {
                    return true;
                }
            }
            return false;
        }

        public List<MedicalAppointmentHistoryDto> GetPatientAppointments(int id)
        {
            List<MedicalAppointmentHistoryDto> medicalAppointmentHistoryDtos = new List<MedicalAppointmentHistoryDto>();
            _medicalAppointmentRepository.GetPatientMedicalAppointments(id).ForEach(appointment => medicalAppointmentHistoryDtos.Add(MedicalAppointmentAdapter.MedicalAppointmentToMedicalAppointmentHistoryDto(appointment)));
            return medicalAppointmentHistoryDtos;
        }
        #endregion
    }

}
