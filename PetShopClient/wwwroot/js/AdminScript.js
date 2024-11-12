document.addEventListener("DOMContentLoaded", function () {
    // Function to load a section dynamically
    function loadSection(target, url) {
        fetch(url, {
            method: 'GET',
            headers: {
                'X-Requested-With': 'XMLHttpRequest' // Indicate this is an AJAX request
            }
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.text();
            })
            .then(html => {
                document.getElementById("dynamicContent").innerHTML = html;
                showSection(target);
            })
            .catch(error => {
                console.error('There was a problem with the fetch operation:', error);
            });
    }

    // Function to show a specific section by ID
    function showSection(sectionId) {
        document.querySelectorAll('.dashboard-section').forEach(section => {
            section.style.display = section.id === sectionId ? 'block' : 'none';
        });
    }

    // Map data-target attributes to exact action names in the controller
    const sectionMapping = {
        overview: "OverviewSection",
        users: "UserSection",
        PetService: "PetServiceSection",
        Products: "ProductSection",
        Events: "EventSection",
        Invoice: "InvoiceSection",
        PlannedServices: "PlannedServiceSection",
        Payment: "IncomeSection",
        settings: "ProfileSection",
        Facility : "FacilitySection"
    };

    // Event listeners for sidebar items
    document.querySelectorAll('.sidebar ul li a').forEach(link => {
        link.addEventListener("click", function (e) {
            e.preventDefault();
            const targetSection = this.getAttribute("data-target");

            // Use mapping to get the correct URL
            const actionName = sectionMapping[targetSection];
            if (actionName) {
                const url = `/Admin/${actionName}`;
                loadSection(targetSection, url);
            } else {
                console.error(`No mapping found for section: ${targetSection}`);
            }
        });
    });
});
