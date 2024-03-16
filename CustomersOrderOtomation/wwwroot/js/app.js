function getCategoriesForShopPage() {
    $.ajax({
        url: '/Category/GetCategories',
        type: 'GET',
        dataType: 'json',
        success: function (data) {

            let categoryListHtml = '';

            for (let i = 0; i < data.length; i++) {

                categoryListHtml += '<li class="mb-1"><a href="GetProductsByCategory?categoryId=' + data[i].id + '"  class="d-flex"><span>' + data[i].name + '</span><span class="text-black ml-auto"></span></a></li>'

            }

            $("#ulCategorListField").html(categoryListHtml);


        },
        error: function (exc) {

        }
    });
}

function EditThisProduct(productId) {
    $("#hdnProductIdProductModal").val(productId);
    $("#productCreateEditModal").css("display", "block");
}

function CloseModal(modalName) {
    $("#" + modalName).css("display", "none");
}

function SaveProductInformation() {

    var productName = $("#inProductNameProductModal").val();

    if (productName == "" || productName.length < 3) {

        new Notify({
            title: 'Hata',
            text: 'Product name must be 3 character or more. ',
            status: 'error',
            autoclose: false,
        })

        return false;
    }

    var productPrice = $("#inProductPriceProductModal").val();


    if (productPrice == "" || parseFloat(productPrice) <= 0) {

        new Notify({
            title: 'Hata',
            text: 'Product price must be greater than 0. ',
            status: 'error',
            autoclose: true,
            autotimeout: 2000
        })

        return false;
    }

    var productId = $("#hdnProductIdProductModal").val();

    var formData = {
        productId: productId,
        productName: productName,
        productPrice: productPrice
    };

    $.ajax({
        url: '/Management/CreateOrUpdateProduct',
        type: 'POST',
        data: formData,
 
        success: function (data) {

            if (data = "basarili") {


                new Notify({
                    title: 'Sipariş Onaylandı',
                    text: 'Siparişiniz alınmıştır.',
                    status: 'success',
                    autoclose: true,
                    autotimeout: 2000
                })

            }

            else {
                new Notify({
                    title: 'Hata',
                    text: 'Sipariş Alınırken Hata Oluştu.',
                    status: 'error',
                    autoclose: false,
                })
            }



        },
        error: function (exc) {
            new Notify({
                title: 'Hata',
                text: 'Sipariş Alınırken Hata Oluştu.',
                status: 'error',
                autoclose: false,
            })
        }
    });
}