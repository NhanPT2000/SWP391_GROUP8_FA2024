const loginForm = document.querySelector('.header .login-form');
const loginBtn = document.querySelector('#login-btn');
const menuBtn = document.querySelector('#menu-btn');
const navbar = document.querySelector('.header .navbar');
const header = document.querySelector('.header');
const submitReviewBtn = document.querySelector('#submit-review');
const reviewText = document.querySelector('#review-text');
const reviewsSection = document.querySelector('.reviews');

let lastScrollTop = 0;

window.addEventListener('scroll', () => {
    let scrollTop = window.pageYOffset || document.documentElement.scrollTop;
    if (scrollTop > lastScrollTop) {
        // Scroll down
        header.style.transform = 'translateY(0)';
    } else {
        // Scroll up
        header.style.transform = 'translateY(-100%)';
    }
    lastScrollTop = scrollTop;
});
loginBtn.onclick = () => {
    loginForm.classList.toggle('active');
    navbar.classList.remove('active');
};

menuBtn.onclick = () => {
    navbar.classList.toggle('active');
    loginForm.classList.remove('active');
};

submitReviewBtn.onclick = () => {
    const reviewContent = reviewText.value.trim();
    if (reviewContent) {
        const newReview = document.createElement('div');
        newReview.classList.add('review');
        newReview.innerHTML = `
            <p><strong>You:</strong> ${reviewContent}</p>
            <button class="btn reply-btn">Reply</button>
        `;
        reviewsSection.insertBefore(newReview, reviewsSection.querySelector('.review-form'));
        reviewText.value = '';
    }
};

document.addEventListener('click', function (e) {
    if (e.target && e.target.classList.contains('reply-btn')) {
        const reviewDiv = e.target.closest('.review');
        const existingReplyForm = reviewDiv.querySelector('.reply-form');
        if (!existingReplyForm) {
            const replyForm = document.createElement('div');
            replyForm.classList.add('reply-form');
            replyForm.innerHTML = `
                <textarea class="reply-text" placeholder="Write your reply here..."></textarea>
                <button class="btn submit-reply">Submit Reply</button>
            `;
            reviewDiv.appendChild(replyForm);
        }
    }
});

document.addEventListener('click', function (e) {
    if (e.target && e.target.classList.contains('submit-reply')) {
        const replyText = e.target.previousElementSibling.value.trim();
        if (replyText) {
            const replyDiv = document.createElement('div');
            replyDiv.classList.add('reply');
            replyDiv.innerHTML = `<p><strong>You:</strong> ${replyText}</p>`;
            e.target.closest('.review').appendChild(replyDiv);
            e.target.closest('.reply-form').remove(); // Remove the reply form after submitting
        }
    }
});
