$(function () {

    $(document).on('click', '#addFacilityBtn', function () {
        $.ajax({
            url: '/Admin/FacilityForm',
            type: 'GET',
            success: function (data) {
                $('#facilityFormContainer').html(data);
                $('#facilityModal').show();
            },
            error: function () {
                alert('Failed to load the facility form.');
            }
        });
    });


    $(document).on('click', '#editFacilitybtn', function () {
        var facilityId = $(this).data('id');
        $.ajax({
            url: '/Admin/UpdateFacilityForm/' + facilityId,
            type: 'GET',
            success: function (data) {
                $('#facilityFormContainer').html(data);
                $('#facilityModal').show();
            },
            error: function () {
                alert('Failed to load the update facility form.');
            }
        });
    });


    $(document).on('submit', '#facilityForm', function (event) {
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
                    loadFacilities();
                    $('#facilityModal').hide();
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert('Failed to save the facility.');
            }
        });
    });


    $(document).on('submit', '#facilityFormUpdate', function (event) {
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
                    loadFacilities();
                    $('#facilityModal').hide();
                } else {
                    alert(response.error || 'Failed to update the facility.');
                }
            },
            error: function () {
                alert('Failed to update the facility.');
            }
        });
    });


    $(document).on('click', '#deleteFacilitybtn', function () {
        var facilityId = $(this).data('id');
        if (confirm("Do you want to delete this facility permanently?")) {
            $.ajax({
                url: '/Admin/DeleteFacility/' + facilityId,
                type: 'DELETE',
                success: function (response) {
                    if (response.success) {
                        loadFacilities();
                    } else {
                        alert('Failed to delete the facility.');
                    }
                },
                error: function () {
                    alert('Failed to delete the facility.');
                }
            });
        }
    });

    function loadFacilities() {
        $.ajax({
            url: '/Admin/FacilitySection',
            type: 'GET',
            success: function (data) {
                $('#facilityTable tbody').html(data);
            },
            error: function () {
                alert('Failed to load facilities.');
            }
        });
    }

    $(document).on('click', function (event) {
        if ($(event.target).is('#facilityModal')) {
            $('#facilityModal').hide();
        }
    });
});