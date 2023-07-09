// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//Function for clearing the textboxes  
function clearTextBox() {
    $('#ValidationSummary').text('');
    $('#Make').val("");
    $('#Model').val("");
    $('#Year').val("");
    $('#Horsepower').val("");
    $('#Acceleration').val("");
    $('#Weight').val("");
    $('#Image').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
}

//Add Data Function   
function Add() {
    var files = $('#ImageUpload').prop("files");
    formData = new FormData();

    formData.append("Make", $('#Make').val());
    formData.append("Model", $('#Model').val());
    formData.append("Year", $('#Year').val());
    formData.append("Horsepower", $('#Horsepower').val());
    formData.append("Acceleration", $('#Acceleration').val());
    formData.append("Weight", $('#Weight').val());
    formData.append("Image", files[0]);

    $.ajax({
        url: "/Administration/CreateCar",
        data: formData,
        type: "POST",
        contentType: false,
        processData: false,
        success: function (result) {
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            $('#ValidationSummary').text('');

            $.each(errormessage.responseJSON, function (key, item) {
                $('#ValidationSummary').append(item);
                $('#ValidationSummary').append('<br>');
            });
        }
    });
}