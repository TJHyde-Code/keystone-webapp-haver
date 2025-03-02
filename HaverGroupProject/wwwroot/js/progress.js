/*
 * Updates the progress display to show the percentage value.
 * Converts the input value (0.0 - 1.0) to a percentage (0% - 100%).
 * If the input is empty or invalid, defaults to "0%".
 */
function updateProgressDisplay() {
    let input = document.getElementById("progressInput");
    let display = document.getElementById("progressDisplay");
    let value = parseFloat(input.value);

    if (!isNaN(value)) {
        display.innerText = Math.round(value * 100) + "%";
    } else {
        display.innerText = "0%";
    }
}

document.addEventListener("DOMContentLoaded", function () {
    updateProgressDisplay();
});
