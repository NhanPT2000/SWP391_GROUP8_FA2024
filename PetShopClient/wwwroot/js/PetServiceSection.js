$(function () {
    // Show add pet service form
    $(document).on('click', '#addPetServiceBtn', function () {
        $.ajax({
            url: '/Admin/ServiceForm',
            type: 'GET',
            success: function (data) {
                $('#serviceFormContainer').html(data);
                $('#petServiceModal').show();
            },
            error: function () {
                alert('Failed to load the pet service form.');
            }
        });
    });

    // Show edit pet service form
    $(document).on('click', '.edit-btn', function () {
        var serviceId = $(this).data('id');
        $.ajax({
            url: '/Admin/UpdateServiceForm/' + serviceId,
            type: 'GET',
            success: function (data) {
                $('#serviceFormContainer').html(data);
                $('#petServiceModal').show();
            },
            error: function () {
                alert('Failed to load the update pet service form.');
            }
        });
    });

    // Submit pet service add form
    $(document).on('submit', '#petServiceForm', function (event) {
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
                    loadPetServices();
                    $('#petServiceModal').hide();
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert('Failed to save the pet service.');
            }
        });
    });

    // Submit pet service update form
    $(document).on('submit', '#petServiceFormUpdate', function (event) {
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
                    loadPetServices();
                    $('#petServiceModal').hide();
                } else {
                    alert(response.error || 'Failed to update the pet service.');
                }
            },
            error: function () {
                alert('Failed to update the pet service.');
            }
        });
    });

    // Delete pet service
    $(document).on('click', '.delete-btn', function () {
        var serviceId = $(this).data('id');
        if (confirm("Do you want to delete this pet service permanently?")) {
            $.ajax({
                url: '/Admin/DeleteService/' + serviceId,
                type: 'DELETE',
                success: function (response) {
                    if (response.success) {
                        loadPetServices();
                    } else {
                        alert('Failed to delete the pet service.');
                    }
                },
                error: function () {
                    alert('Failed to delete the pet service.');
                }
            });
        }
    });

    // Load pet services
    function loadPetServices() {
        $.ajax({
            url: '/Admin/PetServiceSection',
            type: 'GET',
            success: function (data) {
                $('#petServiceTable tbody').html(data);
            },
            error: function () {
                alert('Failed to load pet services.');
            }
        });
    }

    // Close modal when clicking outside of it
    $(document).on('click', function (event) {
        if ($(event.target).is('#petServiceModal')) {
            $('#petServiceModal').hide();
        }
    });
});