//FileUpload validation
function ValidateFileUpload() {
    const input = document.getElementById('inputGroupFile');
    const files = input.files;
    const allowedExtensions = /(\.jpg|\.jpeg|\.png|\.pdf)$/i;
    const maxFileSizeInBytes = 10485760; // Maximum file size in bytes (10 MB)
    const errorMessagesDiv = document.getElementById('errorMessages');
    let errorMessage = '';

    for (let i = 0; i < files.length; i++) {
        const file = files[i];
        const fileSizeInBytes = file.size;

        // Check file extension
        if (!allowedExtensions.test(file.name)) {
            errorMessage += `<div class="text-danger" >Only (.jpg ,.jpeg ,.png ,.pdf) File type allowed </div>`;
            input.value = ''; // Clear selected file
        }

        // Check file size
        if (fileSizeInBytes > maxFileSizeInBytes) {
            errorMessage += `<div class="text-danger">File size exceeds the limit (<10 MB) Allowed</div>`;
            input.value = ''; // Clear selected file
        }
    }

    errorMessagesDiv.innerHTML = errorMessage;
    if (errorMessage !== '') {
        return false;
    }
    return true;
}

//Location Scripted

$(document).ready(function () {
    $("#openBtn").click(function () {
        $("#myModal1").modal("show");
        var Street = $("#street").val();
        var City = $("#city").val();
        var State = $("#state").val();
        var ZipCode = $("#zipcode").val();
        var address = "https://maps.google.com/maps?q=" + Street + City + State + ZipCode + "&t=&z=13&ie=UTF8&iwloc=&output=embed";
        $("#gmap_canvas").attr("src", address);
    });
});

$(document).ready(function () {
    $("#closeBtn1").click(function () {
        $("#myModal1").modal("hide");
    });
});

