$(document).ready(function () {
    ShowCheckoutItems();

    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = $('#quantity_value').text();
        if (quantity != "") {
            quantity = parseInt(quantity);
        } else {
            quantity = 1
        }
     
        $.ajax({
            url: '/shoppingcart/addtocart',
            type: 'POST',
            data: {id:id,quantity:quantity},
            success: function (rs) {
                if (rs.success) {
                    $('#checkout_items').html(rs.count);
                    alert(rs.msg);

                }
            }
        })
    });

    $('body').on('click', '.btnDeleteProduct', function (e) {
        e.preventDefault();
        var conf = confirm("Bạn có muốn xóa sản phẩm khỏi giỏ hàng không?");
        if (conf == true) {
            var id = $(this).data('id');
            $.ajax({
                url: '/shoppingcart/removeitem',
                type: 'POST',
                data: { id: id },
                success: function (rs) {
                    if (rs.success) {
                        $('#checkout_items').html(rs.count);
                        location.reload();
                    }
                }
            })
        }
    });

    $('body').on('click', '.btnDeleteAllProduct', function (e) {
        e.preventDefault();
        var conf = confirm("Bạn có muốn xóa tất cả sản phẩm khỏi giỏ hàng không?");
        if (conf == true) {
            DeleteAllProduct();
        }
    });

    $('body').on('click', '.btnUpdateProduct', function (e) {
        e.preventDefault();
        var conf = confirm("Xác nhận thay đổi số lượng sản phẩm!");
        if (conf == true) {
            var id = $(this).data('id');
            var quantity = $('#ProductQuantity_' + id).val();
            UpdateProductQuantity(id, quantity);
        }
    });
});

function ShowCheckoutItems() {
    $.ajax({
        url: '/shoppingcart/showcheckoutitems',
        type: 'GET',
        success: function (rs) {
            if (rs.success) {
                $('#checkout_items').html(rs.count);    
            }
        }
    })
}

function DeleteAllProduct() {
    $.ajax({
        url: '/shoppingcart/RemoveAll',
        type: 'POST',
        success: function (rs) {
            if (rs.success) {
                $('#checkout_items').html(rs.count);
                location.reload();
            }
        }
    })
}

function UpdateProductQuantity(id,quantity) {
    $.ajax({
        url: '/shoppingcart/UpdateProductQuantity',
        type: 'POST',
        data: {id:id,quantity:quantity},
        success: function (rs) {
            if (rs.success) {
                $('#checkout_items').html(rs.count);
                location.reload();
            }
        }
    })
}