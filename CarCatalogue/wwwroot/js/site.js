// Reloading car data
function ReloadData() {
    $.ajax({
        url: "/car/recent",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                var td = 
                html += '<tr>';
                html += '<td class="td-card">' + '<img class="img-card" src="' + item.imageUrl + '" />' + '</td>';
                html += '<td>' + item.id + '</td>';
                html += '<td>' + item.make + '</td>';
                html += '<td>' + item.model + '</td>';
                html += '<td>' + item.year + '</td>';
                html += '<td>' + item.horsepower + '</td>';
                html += '<td>' + item.acceleration + '</td>';
                html += '<td>' + item.weight + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.id + ')">Edit</a> | <a href="#" onclick="Delete(' + item.id + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

// Clearing the textboxes  
function clearTextBox() {
    $('#ValidationSummary').text('');
    $('#Id').val("");
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

// Setting default values
function setDefaultValues() {
    if ($('#Horsepower').val() === '') {
        "Horsepower", $('#Horsepower').val(0);
    }
    if ($('#Acceleration').val() === '') {
        "Acceleration", $('#Acceleration').val(0);
    }
    if ($('#Weight').val() === '') {
        "Weight", $('#Weight').val(0);
    }
}

// Add Data   
function Add() {
    setDefaultValues();

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
            ReloadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            $('#ValidationSummary').text('');
            setDefaultValues();

            $.each(errormessage.responseJSON, function (key, item) {
                $('#ValidationSummary').append(item);
                $('#ValidationSummary').append('<br>');
            });
        }
    });
}

// Edit Data
function Update() {
    var files = $('#ImageUpload').prop("files");
    formData = new FormData();

    formData.append("Id", $('#Id').val());
    formData.append("Make", $('#Make').val());
    formData.append("Model", $('#Model').val());
    formData.append("Year", $('#Year').val());
    formData.append("Horsepower", $('#Horsepower').val());
    formData.append("Acceleration", $('#Acceleration').val());
    formData.append("Weight", $('#Weight').val());
    formData.append("Image", files[0]);

    $.ajax({
        url: "/Administration/UpdateCar",
        data: formData,
        type: "POST",
        contentType: false,
        processData: false,
        success: function (result) {
            ReloadData();
            clearTextBox();
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

// Pre-filling the Data Based upon Car Id  
function getbyID(carId) {
    $('#Make').css('border-color', 'lightgrey');
    $('#Model').css('border-color', 'lightgrey');
    $('#Year').css('border-color', 'lightgrey');
    $('#Horsepower').css('border-color', 'lightgrey');
    $('#Acceleration').css('border-color', 'lightgrey');
    $('#Weight').css('border-color', 'lightgrey');

    $.ajax({
        url: "/car/" + carId + "/false/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Id').val(result.id);
            $('#Make').val(result.make);
            $('#Model').val(result.model);
            $('#Year').val(result.year);
            $('#Horsepower').val(result.horsepower);
            $('#Acceleration').val(result.acceleration);
            $('#Weight').val(result.weight);

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function Delete(Id) {
    var confirmation = confirm("Are you sure you want to delete this Record?");
    if (confirmation) {
        $.ajax({
            url: "/Administration/DeleteCar/" + Id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                ReloadData();
            },
            error: function (errormessage) {
                ReloadData();
            }
        });
    }
}  