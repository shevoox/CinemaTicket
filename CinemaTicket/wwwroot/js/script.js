// Custom Cursor
const cursor = document.querySelector('.cursor');
let mouseX = 0;
let mouseY = 0;
let cursorX = 0;
let cursorY = 0;
const easing = 0.2;

function animateCursor() {
    const distX = mouseX - cursorX;
    const distY = mouseY - cursorY;
    
    cursorX += distX * easing;
    cursorY += distY * easing;
    
    cursor.style.left = `${cursorX}px`;
    cursor.style.top = `${cursorY}px`;
    
    requestAnimationFrame(animateCursor);
}

document.addEventListener('mousemove', (e) => {
    mouseX = e.clientX;
    mouseY = e.clientY;
});

const clickables = document.querySelectorAll('a, button, .movie-card');
clickables.forEach(element => {
    element.addEventListener('mouseenter', () => {
        cursor.classList.add('active');
    });
    element.addEventListener('mouseleave', () => {
        cursor.classList.remove('active');
    });
});

animateCursor();

// Animate movie cards when they come into view
const observerOptions = {
    root: null,
    rootMargin: '0px',
    threshold: 0.1
};

const observer = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            const card = entry.target;
            const index = Array.from(card.parentNode.children).indexOf(card);
            card.style.animationDelay = `${index * 0.1}s`;
            card.style.opacity = '1';
            card.style.transform = 'translateY(0)';
            observer.unobserve(card);
        }
    });
}, observerOptions);

document.querySelectorAll('.movie-card').forEach(card => {
    card.style.opacity = '0';
    card.style.transform = 'translateY(20px)';
    observer.observe(card);
});

// Add hover effect to navigation links
const navLinks = document.querySelectorAll('nav a');
navLinks.forEach(link => {
    link.addEventListener('click', (e) => {
        navLinks.forEach(l => l.classList.remove('active'));
        link.classList.add('active');
    });
});

// Add subtle parallax effect to hero section
const hero = document.querySelector('.hero');
window.addEventListener('scroll', () => {
    const scrollPosition = window.scrollY;
    if (scrollPosition < window.innerHeight) {
        hero.style.backgroundPositionY = `${scrollPosition * 0.5}px`;
    }
}); 