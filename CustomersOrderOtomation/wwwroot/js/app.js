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

    $.ajax({
        url: '/Management/GetSingleProductByIdForManagement',
        type: 'GET',
        data: { productId: productId },
        dataType: 'json',
        success: function (data) {

            if (data != null) {

                $("#inProductNameProductModal").val(data.name);
                $("#inProductPriceProductModal").val(data.price);
                $("#drpProductStatus").val(data.isActive.toString());
                ShowModal('productCreateEditModal');
            }


        },
        error: function (exc) {

        }
    });
}

function CloseModal(modalId) {
    const modal = bootstrap.Modal.getInstance(document.getElementById(modalId));
    modal.hide();
}

function ShowModal(modalId) {
    const modal = new bootstrap.Modal(document.getElementById(modalId));
    modal.show();
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
    var productStatus = $("#drpProductStatus").val();

    var formData = {
        productId: productId,
        productName: productName,
        productPrice: productPrice,
        productStatus: productStatus
    };

    $.ajax({
        url: '/Management/CreateOrUpdateProduct',
        type: 'POST',
        data: formData,

        success: function (data) {

            if (data) {
                new Notify({
                    title: 'Ürün Bilgileri Kaydedildi',
                    text: 'Ürün Bilgileri Kaydedildi.',
                    status: 'success',
                    autoclose: true,
                    autotimeout: 2000
                })

                CloseModal("productCreateEditModal");

                loadProducts();

            }

            else {
                new Notify({
                    title: 'Hata',
                    text: 'Ürün Bilgileri Kaydedilirken Hata Oluştu.',
                    status: 'error',
                    autoclose: false,
                })
            }



        },
        error: function (exc) {
            new Notify({
                title: 'Hata',
                text: 'Ürün Bilgileri Kaydedilirken Hata Oluştu.',
                status: 'error',
                autoclose: false,
            })
        }
    });
}

function SaveCategoryInformation() {

    var categoryName = $("#inCategoryNameCategoryModal").val();

    if (categoryName == "" || categoryName.length < 3) {

        new Notify({
            title: 'Hata',
            text: 'Categoy name must be 3 character or more. ',
            status: 'error',
            autoclose: false,
        })

        return false;
    }

    var categoryId = $("#hdnCategoryIdCategoryModal").val();
    var categoryStatus = $("#drpCategoryStatus").val();

    var formData = {
        categoryId: categoryId,
        categoryName: categoryName,
        categoryStatus: categoryStatus
    };

    $.ajax({
        url: '/Management/CreateOrUpdateCategory',
        type: 'POST',
        data: formData,

        success: function (data) {

            if (data) {
                new Notify({
                    title: 'Kategori Bilgileri Kaydedildi',
                    text: 'Kategori Bilgileri Kaydedildi.',
                    status: 'success',
                    autoclose: true,
                    autotimeout: 2000
                })

                CloseModal("categoryCreateEditModal");

                loadCategories();

        

            }

            else {
                new Notify({
                    title: 'Hata',
                    text: 'Kategori Bilgileri Kaydedilirken Hata Oluştu.',
                    status: 'error',
                    autoclose: false,
                })
            }



        },
        error: function (exc) {
            new Notify({
                title: 'Hata',
                text: 'Kategori Bilgileri Kaydedilirken Hata Oluştu.',
                status: 'error',
                autoclose: false,
            })
        }
    });
}

function EditThisCategory(categoryId) {
    $("#hdnCategoryIdCategoryModal").val(categoryId);

    $.ajax({
        url: '/Management/GetSingleCategoryByIdForManagement',
        type: 'GET',
        data: { categoryId: categoryId },
        dataType: 'json',
        success: function (data) {

            if (data != null) {

                $("#inCategoryNameCategoryModal").val(data.name);
                $("#drpCategoryStatus").val(data.isActive.toString());
                ShowModal('categoryCreateEditModal');
            }

        },
        error: function (exc) {

        }
    });

}

function AddNewProduct() {
    $("#hdnProductIdProductModal").val(0);
    ShowModal('productCreateEditModal');
}

function loadProducts() {
    $.ajax({
        url: '/Management/GetAllProducts', 
        method: 'GET',
        success: function (response) {
            // Tabloyu temizle
            $('#productsTable tbody').empty();

            // Veriyi tabloya ekle
            response.forEach(function (item) {
                var row = `<tr>
                        <td>
                            <div class="d-flex align-items-center">
                                <div class="ms-3">
                                    <p class="fw-bold mb-1">${item.name}</p>
                                </div>
                            </div>
                        </td>
                        <td>
                            <p class="fw-normal mb-1">${item.price} TL</p>
                        </td>
                        <td>
                            <span class="badge ${item.isActive ? 'bg-success' : 'bg-danger'}">
                                ${item.isActive ? 'Yes' : 'No'}
                            </span>
                        </td>
                        <td>
                            <button type="button" class="btn btn-primary btn-sm rounded-pill px-3" onclick="EditThisProduct(${item.id})">
                                <i class="fas fa-edit"></i> Edit
                            </button>
                        </td>
                    </tr>`;
                $('#productsTable tbody').append(row);
            });
        },
        error: function () {
            alert("Error loading products");
        }
    });
}

function loadCategories() {
    $.ajax({
        url: '/Management/GetAllCategories',
        method: 'GET',
        success: function (response) {
            // Tabloyu temizle
            $('#categoriesTable tbody').empty();

            // Veriyi tabloya ekle
            response.forEach(function (item) {
                var row = `<tr>
                        <td>
                            <div class="d-flex align-items-center">
                                <div class="ms-3">
                                    <p class="fw-bold mb-1">${item.name}</p>
                                </div>
                            </div>
                        </td>
                        <td>
                            <span class="badge ${item.isActive ? 'bg-success' : 'bg-danger'}">
                                ${item.isActive ? 'Yes' : 'No'}
                            </span>
                        </td>
                        <td>
                            <button type="button" class="btn btn-primary btn-sm rounded-pill px-3" onclick="EditThisCategory(${item.id})">
                                <i class="fas fa-edit"></i> Edit
                            </button>
                        </td>
                    </tr>`;
                $('#categoriesTable tbody').append(row);
            });
        },
        error: function () {
            alert("Error loading products");
        }
    });
}

function AddNewCategory() {
    $("#hdnCategoryIdCategoryModal").val(0);
    ShowModal('categoryCreateEditModal');
}