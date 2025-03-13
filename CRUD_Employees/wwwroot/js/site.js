// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", function () {
    var contractTypeDropdown = document.getElementById("contractTypeDropdown");
    var contractDueInput = document.getElementById("contractDueInput");
    var form = document.querySelector("form");
    var errorMessage = setupErrorMessage();

    function setupErrorMessage() {
        var existingError = document.getElementById("contractErrorMessage");
        if (!existingError) {
            var errorSpan = document.createElement("span");
            errorSpan.id = "contractErrorMessage";
            errorSpan.style.color = "red";
            errorSpan.style.display = "none";
            contractDueInput.parentElement.appendChild(errorSpan);
            return errorSpan;
        }
        return existingError;
    }

    function toggleContractDue() {
        if (contractTypeDropdown.value === "Permanent") {
            disableContractDueInput();
        } else {
            enableContractDueInput();
        }
    }

    function disableContractDueInput() {
        contractDueInput.value = "";
        contractDueInput.disabled = true;
        hideErrorMessage();
    }

    function enableContractDueInput() {
        contractDueInput.disabled = false;
    }

    function hideErrorMessage() {
        errorMessage.textContent = "";
        errorMessage.style.display = "none";
    }

    function showErrorMessage(message) {
        errorMessage.textContent = message;
        errorMessage.style.display = "inline";
    }

    function validateContractDue(event) {
        hideErrorMessage();

        if (contractTypeDropdown.value !== "Permanent") {
            if (!contractDueInput.value) {
                showErrorMessage("Contract due date is required when the contract is not Permanent.");
                event.preventDefault();
                return;
            }

            if (isPastDate(contractDueInput.value)) {
                showErrorMessage("Contract due date cannot be in the past.");
                event.preventDefault();
            }
        }
    }

    function isPastDate(dateString) {
        var contractDueDate = new Date(dateString);
        var currentDate = new Date();
        currentDate.setHours(0, 0, 0, 0);
        return contractDueDate < currentDate;
    }

    // Event listeners
    form.addEventListener("submit", validateContractDue);
    contractTypeDropdown.addEventListener("change", toggleContractDue);
    toggleContractDue();
});

  
// Form submission validation for birth year
document.addEventListener("DOMContentLoaded", function () {
    // Custom validation for BirthYear field
    const birthYearInput = document.querySelector("input[name='BirthYear']");
    const form = document.querySelector("form");

    if (form) {
        form.onsubmit = function (event) {
            const birthYear = birthYearInput.value;

            // Check if the birth year is a valid number and within the correct range
            const minYear = new Date().getFullYear() - 66;
            const maxYear = new Date().getFullYear() - 18;

            if (isNaN(birthYear) || birthYear < minYear || birthYear > maxYear) {
                event.preventDefault(); // Prevent form submission
                alert("Please enter a valid birth year between 18 and 66 years old.");
            }
        };
    }
});
    