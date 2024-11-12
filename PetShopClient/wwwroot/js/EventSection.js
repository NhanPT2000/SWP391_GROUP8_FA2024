$(function () {
    $(document).on('click', '#addEventBtn', function () {
        $.ajax({
            url: '/Admin/EventForm',
            type: 'GET',
            success: function (data) {
                $('#eventFormContainer').html(data);
                $('#eventModal').show();
            },
            error: function () {
                alert('Failed to load the event form.');
            }
        });
    });

    $(document).on('click', '.editEventBtn', function () {
        var eventId = $(this).data('id');
        $.ajax({
            url: '/Admin/UpdateEventForm/' + eventId,
            type: 'GET',
            success: function (data) {
                $('#eventFormContainer').html(data);
                $('#eventModal').show();
            },
            error: function () {
                alert('Failed to load the update event form.');
            }
        });
    });

    $(document).on('submit', '#eventForm', function (event) {
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
                    loadEvents();
                    $('#eventModal').hide();
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert('Failed to save the event.');
            }
        });
    });

    $(document).on('submit', '#eventFormUpdate', function (event) {
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
                    loadEvents();
                    $('#eventModal').hide();
                } else {
                    alert(response.error || 'Failed to update the event.');
                }
            },
            error: function () {
                alert('Failed to update the event.');
            }
        });
    });

    $(document).on('click', '.deleteEventBtn', function () {
        var eventId = $(this).data('id');
        if (confirm("Do you want to delete this event permanently?")) {
            $.ajax({
                url: '/Admin/DeleteEvent/' + eventId,
                type: 'DELETE',
                success: function (response) {
                    loadEvents();
                },
                error: function () {
                    alert('Failed to delete the event.');
                }
            });
        }
    });

    function loadEvents() {
        $.ajax({
            url: '/Admin/EventSection',
            type: 'GET',
            success: function (data) {
                $('#eventsTable tbody').html(data);
            },
            error: function () {
                alert('Failed to load events.');
            }
        });
    }

    $(document).on('click', '#closeEventModalBtn', function () {
        $('#eventModal').hide();
    });

    $(document).on('click', function (event) {
        if ($(event.target).is('#eventModal')) {
            $('#eventModal').hide();
        }
    });
});
