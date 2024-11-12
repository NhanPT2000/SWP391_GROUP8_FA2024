$(function () {
    $(document).on('click', '#deleteServiceBtn', function () {
        var plannedServiceId = $(this).data('id');
        if (confirm("Do you want to delete this planned service?")) {
            $.ajax({
                url: '/Admin/DeletePlannedService/' + plannedServiceId,
                type: 'GET',
                data: { id: plannedServiceId },
                success: function (response) {
                    loadServices();
                },
                error: function () {
                    alert('Failed to delete the service.');
                }
            });
        }
    });

    $(document).on('click', '#confirmBtn', function () {
        var plannedServiceId = $(this).data('id');
        if (confirm("Do you want to confirm this planned service?")) {
            $.ajax({
                url: '/Admin/ConfirmedPlannedService/' + plannedServiceId,
                type: 'GET',
                data: { id: plannedServiceId },
                success: function (response) {
                    loadServices();
                },
                error: function () {
                    alert('Failed to update the service.');
                }
            });
        }
    });

    $(document).on('click', '#unconfirmBtn', function () {
        var plannedServiceId = $(this).data('id');
        if (confirm("Do you want to remove confirmation from this planned service?")) {
            $.ajax({
                url: '/Admin/UnconfirmedPlannedService/' + plannedServiceId,
                type: 'GET',
                data: { id: plannedServiceId },
                success: function (response) {
                    loadServices();
                },
                error: function () {
                    alert('Failed to update the service.');
                }
            });
        }
    });

    function loadServices() {
        $.ajax({
            url: '/Admin/PlannedServiceSection/',
            type: 'GET',
            success: function (data) {
                $('#servicesTable tbody').html(data);
            },
            error: function () {
                alert('Failed to load planned services.');
            }
        });
    }
});