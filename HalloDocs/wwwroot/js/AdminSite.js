if (document.getElementById("PatientPhoneNumber")) {

    patientInputField = document.getElementById("PatientPhoneNumber");
    patientInputField.addEventListener('countrychange', () => {
        patientvalidatePhoneNumber();
    });
    patientInputFielditi = window.intlTelInput(patientInputField, {
        initialCountry: "in",
        separateDialCode: true,
        utilsScript: "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/17.0.8/js/utils.js",
    });

}
function patientvalidatePhoneNumber() {
    if (patientInputFielditi.isValidNumber()) {
        document.getElementById("patienttextChange").innerHTML = "Valid";
        document.getElementById("patienttextChange").classList.remove("text-danger");
        document.getElementById("patienttextChange").classList.add("text-success");
    } else {
        document.getElementById("patienttextChange").innerHTML = "Invalid";
        document.getElementById("patienttextChange").classList.remove("text-success");
        document.getElementById("patienttextChange").classList.add("text-danger");
    }
    const PatientContrycodeInput = document.getElementById("PatientCountryCode");
    PatientContrycodeInput.value = patientInputFielditi.getSelectedCountryData().dialCode;

    console.log(PatientContrycodeInput.value);
}