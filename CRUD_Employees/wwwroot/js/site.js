// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", function () {
    const contractTypeDropdown = document.getElementById("contractTypeDropdown");
    const contractDueInput = document.getElementById("contractDueInput");
    const startDateInput = document.getElementById("StartedWorking");
    const form = document.querySelector("form");

    if (!contractTypeDropdown || !contractDueInput || !startDateInput || !form) {
        return;
    }

    const errorMessage = setupErrorMessage();

    function setupErrorMessage() {
        let errorSpan = document.getElementById("contractErrorMessage");
        if (!errorSpan) {
            errorSpan = document.createElement("span");
            errorSpan.id = "contractErrorMessage";
            errorSpan.style.color = "red";
            errorSpan.style.display = "none";
            contractDueInput.parentElement.appendChild(errorSpan);
        }
        return errorSpan;
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
            } else if (isPastDate(contractDueInput.value)) {
                showErrorMessage("Contract due date cannot be in the past.");
                event.preventDefault();
            }
        }
    }

    function isPastDate(dateString) {
        const contractDueDate = new Date(dateString);
        const currentDate = new Date();
        currentDate.setHours(0, 0, 0, 0);
        return contractDueDate < currentDate;
    }

    function validateStartDate(event) {
        const startDateValue = startDateInput.value;
        if (startDateValue) {
            const startDate = new Date(startDateValue);
            const today = new Date();
            today.setHours(0, 0, 0, 0);
            if (startDate > today) {
                alert("Start date cannot be in the future.");
                event.preventDefault();
            }
        }
    }

    form.addEventListener("submit", function (event) {
        validateContractDue(event);
        validateStartDate(event);
    });

    contractTypeDropdown.addEventListener("change", toggleContractDue);
    toggleContractDue();
});

document.addEventListener("DOMContentLoaded", function () {
    const birthYearInput = document.querySelector("input[name='BirthYear']");
    const form = document.querySelector("form");

    if (form) {
        form.onsubmit = function (event) {
            const birthYear = birthYearInput.value;
            const minYear = new Date().getFullYear() - 66;
            const maxYear = new Date().getFullYear() - 18;

            if (isNaN(birthYear) || birthYear < minYear || birthYear > maxYear) {
                event.preventDefault();
                alert("Please enter a valid birth year between 18 and 66 years old.");
            }
        };
    }
});



  
document.addEventListener("DOMContentLoaded", function () {
    const birthYearInput = document.querySelector("input[name='BirthYear']");
    const form = document.querySelector("form");

    if (form) {
        form.onsubmit = function (event) {
            const birthYear = birthYearInput.value;
            const minYear = new Date().getFullYear() - 66;
            const maxYear = new Date().getFullYear() - 18;

            if (isNaN(birthYear) || birthYear < minYear || birthYear > maxYear) {
                event.preventDefault();
                alert("Please enter a valid birth year between 18 and 66 years old.");
            }
        };
    }
});
    