import Vue from "vue";
import VueRouter from "vue-router";
import HomePage from "../views/HomePage.vue";
import MedicalAppointment from "../components/medicalAppointment/MedicalAppointment.vue";
import DoctorAppointments from "../components/medicalAppointment/DoctorAppointments.vue";
import PatientAppointments from "../components/medicalAppointment/PatientAppointments.vue";
import PostedPatientFeedbacksAdmin from "../components/patientFeedbacks/PostedPatientFeedbacksAdmin.vue";
import NotPostedPatientFeedbacksAdmin from "../components/patientFeedbacks/NotPostedPatientFeedbacksAdmin.vue";
import AddNewFeedback from "../components/patientFeedbacks/AddNewFeedback.vue";
import BlockPatient from "../components/blockPatient/BlockPatient.vue";

Vue.use(VueRouter);

const routes = [{
        path: "/",
        name: "HomePage",
        component: HomePage,
    },
    {
        path: "/createAppointment",
        name: "Medical Appointment",
        component: MedicalAppointment,
    },
    {
        path: "/doctorAppointments",
        name: "Doctor Appointments",
        component: DoctorAppointments,
    },
    {
        path: "/patientAppointments",
        name: "Patient Appointments",
        component: PatientAppointments,
    },
    {
        path: "/postedPatientFeedbacksAdmin",
        name: "Posted Patient Feedbacks Admin",
        component: PostedPatientFeedbacksAdmin,
    },
    {
        path: "/notPostedPatientFeedbacksAdmin",
        name: "Not Posted Patient Feedbacks Admin",
        component: NotPostedPatientFeedbacksAdmin,
    },
    {
        path: "/addNewFeedback",
        name: "Add New Feedback",
        component: AddNewFeedback,
    },
    {
        path: "/blockpatient",
        name: "Block Patient",
        component: BlockPatient,
    },
];

const router = new VueRouter({
    mode: "history",
    base: process.env.BASE_URL,
    routes,
});

export default router;