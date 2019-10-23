

$(document).ready(function () {

    loadItems();

    $('#add-dollar').on("click", function()
    {
        var moneyInput = Number($('#total-money-input').val())
        moneyInput += 1.00;
        $('#total-money-input').val(moneyInput.toFixed(2))
    })

    $('#add-quarter').on("click", function()
    {
        var moneyInput = Number($('#total-money-input').val())
        moneyInput += 0.25;
        $('#total-money-input').val(moneyInput.toFixed(2))
    })

    $('#add-dime').on("click", function()
    {
        var moneyInput = Number($('#total-money-input').val())
        moneyInput += 0.10;
        $('#total-money-input').val(moneyInput.toFixed(2))
    })

    $('#add-nickel').on("click", function()
    {
        var moneyInput = Number($('#total-money-input').val())
        moneyInput += 0.05;
        $('#total-money-input').val(moneyInput.toFixed(2))
    })

    $('#change-return-button').on("click", function()
    {
        $('#message-display').val('');
        $('#change-display').val('');
        $('#item-display').val('');
    })
});

function loadItems()
{
    $('#items').empty();
    var items = $('#items');

    $.ajax({
        type: 'GET',
        url: 'http://tsg-vending.herokuapp.com/items',
        success: function(itemArray) {
            
            $.each(itemArray, function(index, item)
            {
                    var id = item.id;
                    var name = item.name;
                    var price = item.price;
                    var quantity = item.quantity;

                    var row = "<button type='button' class='btn btn-info col-md-3 item-button' id='" + id + "'onclick='displayItem(" + id + ")'>";
                        row += id + "</br>";
                        row += name + "</br>";
                        row += "$" + price + "</br>";
                        row += quantity + "</button>";

                    items.append(row);
            });
        },
        error: function() {
            errorMessages();
        }
    })
}

function purchaseItem() 
{
    $.ajax({
        type: 'POST',
        url: 'http://tsg-vending.herokuapp.com/money/' + $('#total-money-input').val() + '/item/' + $('#item-display').val(),
        success: function(response) {
            var message = "Thank You!";
            var change = response.quarters + " Quarters,";
                change += response.dimes + " Dimes, ";
                change += response.nickels + " Nickels, and ";
                change += response.pennies + " Pennies";
                $('#message-display').val(message);
                $('#change-display').val(change);
                $('#total-money-input').val("0.00");
                loadItems();
        },
        error: function(xhr, status, response)
        {
            var error = jQuery.parseJSON(xhr.responseText);
            $('#message-display').val(error.message)
        }
    })
}

function displayItem(id)
    {
        $('#item-display').val(id)
    }
