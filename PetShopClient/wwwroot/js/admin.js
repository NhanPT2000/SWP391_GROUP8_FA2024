document.addEventListener('DOMContentLoaded', () => {
    const sidebarLinks = document.querySelectorAll('.sidebar ul li a');
    const sections = document.querySelectorAll('.dashboard-section');
    const usersTableBody = document.querySelector('#usersTable tbody');
    const addUserBtn = document.querySelector('#addUserBtn');
    const userModal = document.querySelector('#userModal');
    const closeModalBtn = document.querySelector('.close');
    const userForm = document.querySelector('#userForm');
    const modalTitle = document.querySelector('#modalTitle');
    let currentUserRow = null;

    // Show/Hide sections on sidebar click
    const hideAllSections = () => sections.forEach(section => section.style.display = 'none');
    const showSection = (sectionId) => document.getElementById(sectionId).style.display = 'block';
    sidebarLinks.forEach(link => link.addEventListener('click', event => {
        event.preventDefault();
        hideAllSections();
        showSection(link.getAttribute('data-target'));
    }));
    hideAllSections();
    showSection('overview');

    // Open modal for adding new user
    addUserBtn.addEventListener('click', () => {
        modalTitle.textContent = 'Add User';
        userForm.reset();
        currentUserRow = null;
        userModal.style.display = 'block';
    });

    // Open modal for editing user
    usersTableBody.addEventListener('click', (event) => {
        if (event.target.classList.contains('edit-btn')) {
            const row = event.target.closest('tr');
            currentUserRow = row;
            modalTitle.textContent = 'Edit User';
            document.querySelector('#name').value = event.target.getAttribute('data-name');
            document.querySelector('#role').value = event.target.getAttribute('data-role');
            document.querySelector('#status').value = event.target.getAttribute('data-status');
            userModal.style.display = 'block';
        }
    });

    // Close modal
    closeModalBtn.addEventListener('click', () => userModal.style.display = 'none');

    // Save or Update User
    userForm.addEventListener('submit', (event) => {
        event.preventDefault();
        const name = document.querySelector('#name').value;
        const role = document.querySelector('#role').value;
        const status = document.querySelector('#status').value;

        if (currentUserRow) {
            // Update existing user
            currentUserRow.children[0].textContent = name;
            currentUserRow.children[1].textContent = role;
            currentUserRow.children[2].textContent = status;
        } else {
            // Add new user
            const newRow = document.createElement('tr');
            newRow.innerHTML = `
                <td>${name}</td>
                <td>${role}</td>
                <td>${status}</td>
                <td>
                    <button class="edit-btn" data-name="${name}" data-role="${role}" data-status="${status}">Edit</button>
                    <button class="delete-btn">Delete</button>
                </td>
            `;
            usersTableBody.appendChild(newRow);
        }

        userModal.style.display = 'none';
    });

    // Delete user
    usersTableBody.addEventListener('click', (event) => {
        if (event.target.classList.contains('delete-btn')) {
            const row = event.target.closest('tr');
            row.remove();
        }
    });

    // Close modal when clicking outside the modal content
    window.onclick = (event) => {
        if (event.target === userModal) {
            userModal.style.display = 'none';
        }
    };
});
