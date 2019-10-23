$(document).ready(function ()
{
    loadDVDs();

    $('#create-button').click(function()
    {
        $('#add-dvd').show();
        $('#top-row').hide();
        $('#dvd-table').hide();
        $('#edit-dvd').hide();
    });

    $('#add-dvd-button').click(function (event){

        var haveValidationErrors = checkAndDisplayValidationErrors($('#add-dvd').find('input'));

        if(haveValidationErrors)
        {
            return false;
        }

        $.ajax({
            type: 'POST',
            url: 'http://localhost:52348/dvd',
            data: JSON.stringify({
                title: $('#add-title').val(),
                realeaseYear: $('#add-release-year').val(),
                director: $('#add-director').val(),
                rating: $('#add-rating').val(),
                notes: $('#add-notes').val()
            }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            'dataType': 'json',
            success: function()
            {
                $('#errorMessages').empty();
                $('#add-title').val('');
                $('#add-release-year').val('');
                $('#add-director').val('');
                $('#add-rating').val('');
                $('#add-notes').val('');
                loadDVDs();
                hideCreateDVD();
            },
            error: function()
            {
                errorMessage();
            }
        })

    })
});

function loadDVDs()
{
    clearDVDTable();
    var dvdList = $('#dvd-list');

    $.ajax({
        type: 'GET',
        url: 'http://localhost:52348/dvds',
        success: function(dvdArray) {
            $.each(dvdArray, function(index, dvd){
                var title = dvd.title;
                var releaseYear = dvd.realeaseYear;
                var director = dvd.director;
                var rating = dvd.rating;
                var dvdId = dvd.dvdId;

                var row = '<tr>';
                    row += '<td><button type="button" class="btn btn-link" style="text-align: left" onclick="displaySingleDVD(' + dvdId + ')"</button>' + title + '</td>';
                    row += '<td style="text-align: center">' + releaseYear + '</td>';
                    row += '<td>' + director + '</td>';
                    row += '<td>' + rating + '</td>';
                    row += '<td><button type="button" class="btn btn-outline-primary" onClick="showEditForm(' + dvdId + ')">Edit</button></td>';
                    row += '<td><button type="button" class="btn btn-outline-danger" data-toggle="modal" data-target="#confirm-modal" onClick="deleteDVD(' + dvdId + ')">Delete</button>';
                    row += '</tr>';

                dvdList.append(row);
            });
        },
        error: function() 
        {
            errorMessage();
        }
    })
}

function displaySingleDVD(dvdId)
{
    $('#errorMessage').empty();
    $('#display-dvd-title').empty();

    $.ajax({
        type: 'GET',
        url: 'http://localhost:52348/dvd/' + dvdId,
        success: function(dvd, status){
            $('#display-dvd-title').append('<h1>' + dvd.title + '</h1>');
            $('#display-release-year').val(dvd.realeaseYear);
            $('#display-director').val(dvd.director);
            $('#display-rating').val(dvd.rating);
            $('#display-notes').val(dvd.notes);
            $('#display-dvd-id').val(dvd.dvdId);
        },
        error: function() {
            errorMessage();
        }
    })
    $('#display-dvd').show();
    $('#add-dvd').hide();
    $('#top-row').hide();
    $('#dvd-table').hide();
    $('#edit-dvd').hide();
}

function searchDVDs() 
{
    clearDVDTable();
    var dvdList = $('#dvd-list');

    $.ajax({
        type: 'GET',
        url: 'http://localhost:52348/dvds/' + $('#search-category').val() + '/' + $('#search-input').val(),
        success: function(dvdArray) {
            $.each(dvdArray, function(index, dvd){
                var title = dvd.title;
                var releaseYear = dvd.realeaseYear;
                var director = dvd.director;
                var rating = dvd.rating;
                var dvdId = dvd.dvdId;

                var row = '<tr>';
                    row += '<td>' + title + '</td>';
                    row += '<td style="text-align: center">' + releaseYear + '</td>';
                    row += '<td>' + director + '</td>';
                    row += '<td>' + rating + '</td>';
                    row += '<td><button type="button" class="btn btn-outline-primary" onClick="showEditForm(' + dvdId + ')">Edit</button></td>';
                    row += '<td><button type="button" class="btn btn-outline-danger" data-toggle="modal" data-target="#confirm-modal" onClick="deleteDVD(' + dvdId + ')">Delete</button>';
                    row += '</tr>';

                dvdList.append(row);
            });
        },
        error: function() 
        {
            $('#errorMessages')
            .append($('<li>')
            .attr({class: 'list-group-item list-group-item-danger'})
            .text('Both Category and Search Term are required.'));
        }
    })
}

function showEditForm(dvdId)
{
    $('#errorMessage').empty();
    $('#edit-dvd').show();
    

    $.ajax({
        type: 'GET',
        url: 'http://localhost:52348/dvd/' + dvdId,
        success: function(dvd, status){
            $('#edit-title').val(dvd.title);
            $('#edit-release-year').val(dvd.realeaseYear);
            $('#edit-director').val(dvd.director);
            $('#edit-rating').val(dvd.rating);
            $('#edit-notes').val(dvd.notes);
            $('#edit-dvd-id').val(dvd.dvdId);
        },
        error: function() {
            errorMessage();
        }
    })
    $('#display-dvd').hide();
    $('#add-dvd').hide();
    $('#top-row').hide();
    $('#dvd-table').hide();
}

function editDVD()
{
    var haveValidationErrors = checkAndDisplayValidationErrors($('#edit-dvd').find('input'));

        if(haveValidationErrors)
        {
            return false;
        }

    $.ajax({
        type: 'PUT',
        url: 'http://localhost:52348/dvd/' + $('#edit-dvd-id').val(),
        data: JSON.stringify({
            dvdId: $('#edit-dvd-id').val(),
            title: $('#edit-title').val(),
            realeaseYear: $('#edit-release-year').val(),
            director: $('#edit-director').val(),
            rating: $('#edit-rating').val(),
            notes: $('#edit-notes').val()
        }),
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        'dataType': 'json',
        success: function() {
            $('#errorMessages').empty();
            hideEditDVD();
            loadDVDs();
        },
        error: function() {
            errorMessage();
        }
    })
}

function hideSingleDVD()
{
    $('#errorMessages').empty();
    $('#add-dvd').hide();
    $('#top-row').show();
    $('#dvd-table').show();
    $('#edit-dvd').hide();
    $('#display-dvd').hide();
}

function hideEditDVD()
{
    $('#errorMessages').empty();
    $('#edit-title').val('');
    $('#edit-release-year').val('');
    $('#edit-director').val('');
    $('#edit-rating').val('');
    $('#edit-notes').val('');

    $('#add-dvd').hide();
    $('#top-row').show();
    $('#dvd-table').show();
    $('#edit-dvd').hide();
    $('#display-dvd').hide();
}

function hideCreateDVD()
{
    $('#errorMessages').empty();
    $('#add-title').val('');
    $('#add-release-year').val('');
    $('#add-director').val('');
    $('#add-rating').val('');
    $('#add-notes').val('');

    $('#add-dvd').hide();
    $('#top-row').show();
    $('#dvd-table').show();
    $('#edit-dvd').hide();
    $('#display-dvd').hide();
}

function deleteDVD(dvdId)
{
    $('#confirm-delete').click(function(event)
    {
        $.ajax({
            type: 'DELETE',
            url: 'http://localhost:52348/dvd/' + dvdId,
            success: function(){
                loadDVDs();
            }
        });
    })
}

function errorMessage()
{
    $('#errorMessages')
        .append($('<li>')
        .attr({class: 'list-group-item list-group-item-danger'})
        .text('Error calling web service.  Please try again later.'));
}

function clearDVDTable()
{
    $('#dvd-list').empty();
}

function checkAndDisplayValidationErrors(input)
{
    $('#errorMessages').empty();

    var errorMessages = [];

    input.each(function() {
        if(!this.validity.valid) {
            var errorField = $('label[for =' + this.id + ']').text();
            errorMessages.push(errorField + ' ' + this.validationMessage);
        }
    });

    if(errorMessages.length > 0) {
        $.each(errorMessages, function(index, message) {
            $('#errorMessages').append($('<li>').attr({class: 'list-group-item list-group-item-danger'}).text(message));
        });
        return true;
    }
    else
    {
        return false;
    }
}