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
        public void saveAppointment(MedicalAppointmentDto medicalAppointmentDto)
        {
            MedicalAppointment medicalAppointment = MedicalAppointmentAdapter.MedicalAppointmentDtoToMedicalsAppointment(medicalAppointmentDto);
            _medicalAppointmentRepository.SaveMedicalAppointment(medicalAppointment);
        }
        public MedicalAppointment findAppointment(MedicalAppointmentDto medicalAppointmentDto, string priority)
        {
            MedicalAppointment medicalAppointment = MedicalAppointmentAdapter.MedicalAppointmentDtoToMedicalsAppointment(medicalAppointmentDto);
            Doctor doctor = _doctorRepository.FindById(medicalAppointmentDto.DoctorId);
            medicalAppointment.Doctor = doctor;
            DateTime dateTime = medicalAppointmentDto.Date;
            if (priority == "time" && doctor.Type == Enums.DoctorType.General)
            {
                DateTime doctorFreeTime = findDoctorTime(doctor, dateTime);
                if (!dateTime.Equals(doctorFreeTime))
                {
                    return null;
                }
            }
            else if (priority.Equals("time") && doctor.Type == Enums.DoctorType.Specialist)
            {
                doctor = findDoctorSpecialist(medicalAppointment);
                medicalAppointment.Doctor = doctor;
                if (doctor != null)
                {
                    medicalAppointment.DoctorId = doctor.Id;
                }
                else {
                    medicalAppointment = null;
                }
                
               
            }
            else
            {
                dateTime = findDoctorTime(doctor, dateTime);
                medicalAppointment.Date = dateTime;
            }

            return medicalAppointment;
        }

        #region heplper_function

        public DateTime findDoctorTime(Doctor doctor, DateTime dateTime)
        {

            List<MedicalAppointment> appointments = _medicalAppointmentRepository.GetDoctorAppointments(doctor.Id);
            Boolean flag = false;
            foreach (MedicalAppointment appointment in appointments)
            {
                if (appointment.Date.Equals(dateTime) && appointment.DoctorId.Equals(doctor.Id))
                {
                    dateTime = dateTime.AddDays(1);
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                findDoctorTime(doctor, dateTime);
            }
            return dateTime;
        }

        public Doctor findDoctorSpecialist(MedicalAppointment medicalAppointment)
        {
            List<MedicalAppointment> appointments = _medicalAppointmentRepository.GetDoctorAppointments(medicalAppointment.DoctorId);
            Console.WriteLine(appointments.Count);
            Doctor doctor = medicalAppointment.Doctor;
            if (appointments.Count == 0)
            {
                Console.WriteLine("USAO");
                return doctor;
            }

            foreach (MedicalAppointment appointment in appointments)
            {
                if (appointment.Date.Equals(medicalAppointment.Date))
                {
                    doctor = changeDoctor(medicalAppointment);
                    break;
                }
            }
            return doctor;
        }

        private Doctor changeDoctor(MedicalAppointment medicalAppointment)
        {
            List<Doctor> specialists = _doctorRepository.DoctorSpecialists();

            Boolean flag = false;
            foreach (Doctor specialist in specialists)
            {
               
                    flag = false;
                    List<MedicalAppointment> appointments = _medicalAppointmentRepository.GetDoctorAppointments(specialist.Id);
                    Console.WriteLine("appointmentss u special" + appointments.Count());
                    foreach (MedicalAppointment appointment in appointments)
                    {
                        String DoctorId = specialist.Id.ToString();

                        if (appointment.Date.Equals(medicalAppointment.Date))
                        {
                            Console.WriteLine("usao u if od change");
                            flag = true;
                            break;
                        }

                    }
                    if (flag == false)
                    {
                        Console.WriteLine("Usao u if za izlaz");
                        return specialist;
                    }

                }
            
            return null;
        }

      
        #endregion
    }

}
