var inputField, inputFielditi, patientInputField, patientInputFielditi;


//Password icone show the passwors or not......
if (document.getElementById("togglePassword")) {

    const togglePassword = document.querySelector("#togglePassword");
    const password = document.querySelector("#password");
    togglePassword.addEventListener("click", function () {
        const type = password.getAttribute("type") === "password" ? "text" : "password";
        password.setAttribute("type", type);
        this.classList.toggle("bi-eye");
    });

}

// Check for existing users password and confirmpassword hide
function checkUser() {
    var email = document.getElementById("Email").value;
    fetch('/Patient/PatientSubmitRequest/CheckEmail/' + email)
        .then(response => response.json())
        .then(data => {
            var PasswordHash = document.getElementById('passdiv');
            var ConfirmPassword = document.getElementById('confpassdiv');
            if (data.exists) {
                PasswordHash.style.display = 'none';
                ConfirmPassword.style.display = 'none';
            }
            else {
                PasswordHash.style.display = 'block';
                ConfirmPassword.style.display = 'block';
            }
        })
        .catch(error => console.error('Error:', error));
}


// file name for uploading the documents and types validationfunction GetFileSizeNameAndType() {    const input = document.getElementById('patientFile');    const files = input.files;    const allowedExtensions = /(\.jpg|\.jpeg|\.png|\.pdf)$/i;    const maxFileSizeInBytes = 10485760; // Maximum file size in bytes (10 MB)    const errorMessagesDiv = document.getElementById('errorMessages');    let errorMessage = '';    for (let i = 0; i < files.length; i++) {        const file = files[i];        const fileSizeInBytes = file.size;        // Check file extension        if (!allowedExtensions.test(file.name)) {            errorMessage += `<div class="text-danger" >Only (.jpg ,.jpeg ,.png ,.pdf) File type allowed </div>`;            input.value = ''; // Clear selected file        }        // Check file size        if (fileSizeInBytes > maxFileSizeInBytes) {            errorMessage += `<div class="text-danger">File size exceeds the limit (<10 MB) Allowed</div>`;            input.value = ''; // Clear selected file        }    }        errorMessagesDiv.innerHTML = errorMessage;    if (errorMessage !== '') {        return false;    }    if (errorMessage == '') {        var fi = document.getElementById('patientFile');        var totalFileSize = 0;        if (fi.files.length > 0) {            document.getElementById('fileName').innerHTML += ' :- Total is' + '<b> ' + fi.files.length + ' </b>';        }        return true;    }}

// pop up box show in family submit butoon information submit......
if (document.getElementById('customModal')) {
    document.addEventListener('DOMContentLoaded', function () {
        var myModal = new bootstrap.Modal(document.getElementById('customModal'));
        myModal.show();
    });
}


//relative phonenumberValidation
if (document.getElementById("PhoneNumber")) {
    inputField = document.getElementById("PhoneNumber");
    inputField.addEventListener('countrychange', () => {
        validatePhoneNumber();
    });
    inputFielditi = window.intlTelInput(inputField, {
        initialCountry: "in",
        formatOnDisplay: false,
        separateDialCode:true,
        utilsScript: "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/17.0.8/js/utils.js",
    });
}
function validatePhoneNumber() {
    if (inputFielditi.isValidNumber()) {
        document.getElementById("textChange").innerHTML = "Valid";
        document.getElementById("textChange").classList.remove("text-danger");
        document.getElementById("textChange").classList.add("text-success");
    } else {
        document.getElementById("textChange").innerHTML = "Invalid";
        document.getElementById("textChange").classList.remove("text-success");
        document.getElementById("textChange").classList.add("text-danger");
    }
    const countryCodeInput = document.getElementById("CountryCode");
    countryCodeInput.value = inputFielditi.getSelectedCountryData().dialCode;
}


//PatientPhonenumber Validation
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


//submit,cancle,edit button
if (document.getElementById("editbtn")) {
    var fields = document.getElementById("profileForm");
    var editbtn = document.getElementById("editbtn");
    var cancelbtn = document.getElementById("cancelbtn");
    var submitbtn = document.getElementById("submitbtn");
    editbtn.addEventListener("click", () => {
        editbtn.classList.add("d-none");
        cancelbtn.classList.remove("d-none");
        submitbtn.classList.remove("d-none");
        for (var i = 0; i < fields.length; i++) {
            fields[i].disabled = false;
        }
        document.getElementById("Email").disabled = true;
    });
}

