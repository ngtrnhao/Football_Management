// Simple animation for CTA button on hover
document.querySelector('.cta-button').addEventListener('mouseover', function () {
    this.style.transform = 'scale(1.1)';
});

document.querySelector('.cta-button').addEventListener('mouseout', function () {
    this.style.transform = 'scale(1)';
});
