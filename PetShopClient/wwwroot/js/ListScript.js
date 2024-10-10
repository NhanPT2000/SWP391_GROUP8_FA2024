let loginForm = document.querySelector('.header .login-form');

document.querySelector('#login-btn').onclick = () =>{
    loginForm.classList.toggle('active');
    navbar.classList.remove('active');
}

let navbar = document.querySelector('.header .navbar');

document.querySelector('#menu-btn').onclick = () =>{
    navbar.classList.toggle('active');    
    loginForm.classList.remove('active');
}

window.onscroll = () =>{
    loginForm.classList.remove('active');
    navbar.classList.remove('active');

    if(window.scrollY > 0){
        document.querySelector('.header').classList.add('active');
    }else{
        document.querySelector('.header').classList.remove('active');
    }
}

window.onload = () =>{
    if(window.scrollY > 0){
        document.querySelector('.header').classList.add('active');
    }else{
        document.querySelector('.header').classList.remove('active');
    }
}
let currentPage = 1;
        const productsPerPage = 5;

        function displayProducts(page) {
            const products = document.querySelectorAll('.product-box');
            const startIndex = (page - 1) * productsPerPage;
            const endIndex = startIndex + productsPerPage;

            products.forEach((product, index) => {
                if (index >= startIndex && index < endIndex) {
                    product.style.display = 'block';
                } else {
                    product.style.display = 'none';
                }
            });

            updatePagination(page);
        }

        function updatePagination(page) {
            const pagination = document.getElementById('pagination');
            pagination.innerHTML = '';
            const pageCount = Math.ceil(document.querySelectorAll('.product-box').length / productsPerPage);

            for (let i = 1; i <= pageCount; i++) {
                pagination.innerHTML += `<a href="#" class="${i === page ? 'active' : ''}" onclick="goToPage(${i})">${i}</a>`;
            }
        }

        function goToPage(page) {
            currentPage = page;
            displayProducts(page);
        }

        function toggleSubcategories(categoryId) {
            const subcategories = document.getElementById(categoryId);
            subcategories.style.display = subcategories.style.display === 'block' ? 'none' : 'block';
        }

        function searchProducts() {
            const searchTerm = document.getElementById('search-input').value.toLowerCase();
            const products = document.querySelectorAll('.product-box');
            products.forEach(product => {
                const productName = product.querySelector('h3').innerText.toLowerCase();
                if (productName.includes(searchTerm)) {
                    product.style.display = 'block';
                } else {
                    product.style.display = 'none';
                }
            });
            updatePaginationAfterSearch();
        }

        function updatePaginationAfterSearch() {
            const visibleProducts = document.querySelectorAll('.product-box:not([style*="display: none"])').length;
            const pagination = document.getElementById('pagination');
            pagination.innerHTML = '';
            const pageCount = Math.ceil(visibleProducts / productsPerPage);

            for (let i = 1; i <= pageCount; i++) {
                pagination.innerHTML += `<a href="#" class="${i === currentPage ? 'active' : ''}" onclick="goToPage(${i})">${i}</a>`;
            }
        }

        // Initialize
        displayProducts(currentPage);
        function toggleSubcategories(id) {
            const subcategories = document.getElementById(id);
            if (subcategories.style.display === 'block') {
                subcategories.style.display = 'none';
            } else {
                subcategories.style.display = 'block';
            }
        }
    
        function searchProducts() {
            const input = document.getElementById('search-input').value.toLowerCase();
            const products = document.querySelectorAll('.product-box');
            
            products.forEach(product => {
                const productName = product.querySelector('h3').innerText.toLowerCase();
                if (productName.includes(input)) {
                    product.style.display = 'block';
                } else {
                    product.style.display = 'none';
                }
            });
        }
    
        function filterProducts(category) {
            const products = document.querySelectorAll('.product-box');
            
            products.forEach(product => {
                if (category === 'All' || product.getAttribute('data-category') === category) {
                    product.style.display = 'block';
                } else {
                    product.style.display = 'none';
                }
            });
        }