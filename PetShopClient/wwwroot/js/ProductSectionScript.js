$(function () {
    $(document).on('click', '#addProductBtn', function () {
        $.ajax({
            url: '/Admin/ProductForm',
            type: 'GET',
            success: function (data) {
                $('#productFormContainer').html(data);
                $('#productModal').show();
            },
            error: function () {
                alert('Failed to load the product form.');
            }
        });
    });

    $(document).on('click', '.editProductBtn', function () {
        var productId = $(this).data('id');
        $.ajax({
            url: '/Admin/UpdateProductForm/' + productId,
            type: 'GET',
            success: function (data) {
                $('#productFormContainer').html(data);
                $('#productModal').show();
            },
            error: function () {
                alert('Failed to load the update product form.');
            }
        });
    });

    $(document).on('submit', '#productForm', function (event) {
        event.preventDefault();
        var actionUrl = $(this).attr('action');
        var formData = new FormData(this);

        $.ajax({
            url: actionUrl,
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                if (response.success) {
                    loadProducts();
                    $('#productModal').hide();
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert('Failed to save the product.');
            }
        });
    });

    $(document).on('submit', '#productFormUpdate', function (event) {
        event.preventDefault();
        var actionUrl = $(this).attr('action');
        var formData = new FormData(this);

        $.ajax({
            url: actionUrl,
            type: 'POST',
            headers: { "X-HTTP-Method-Override": "PUT" },
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                if (response.success) {
                    loadProducts();
                    $('#productModal').hide();
                } else {
                    alert(response.error || 'Failed to update the product.');
                }
            },
            error: function () {
                alert('Failed to update the product.');
            }
        });
    });

    $(document).on('click', '.deleteProductBtn', function () {
        var productId = $(this).data('id');
        if (confirm("Do you want to delete this product permanently?")) {
            $.ajax({
                url: '/Admin/DeleteProduct/' + productId,
                type: 'DELETE',
                success: function (response) {
                    loadProducts();
                },
                error: function () {
                    alert('Failed to delete the product.');
                }
            });
        }
    });

    function loadProducts() {
        $.ajax({
            url: '/Admin/ProductSection',
            type: 'GET',
            success: function (data) {
                $('#productsTable tbody').html(data);
            },
            error: function () {
                alert('Failed to load products.');
            }
        });
    }

    $(document).on('click', '#closeProductModalBtn', function () {
        $('#productModal').hide();
    });

    $(document).on('click', function (event) {
        if ($(event.target).is('#productModal')) {
            $('#productModal').hide();
        }
    });
});
