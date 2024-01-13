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