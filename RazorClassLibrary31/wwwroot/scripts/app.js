function showAlert() {
    alert("This is an alert");
}

function askQuestion(question) {
    var answer = prompt(question);
    return answer;
}

function focusOnInputQuestion(element) {
    element.focus()
}

function closeEmployeeEditModal() {
    $("#employeeEditModal").modal("hide");
}

function setToLocalStorage(key, value) {
    localStorage.setItem(key, value);
}

function getFromLocalStorage(key) {
    return localStorage.getItem(key);
}

function removeFromLocalStorage(key) {
    localStorage.removeItem(key);
}

function clearLocalStorage() {
    localStorage.clear();
}

function setToSessionStorage(key, value) {
    sessionStorage.setItem(key, value);
}

function getFromSessionStorage(key) {
    return sessionStorage.getItem(key);
}

function removeFromSessionStorage(key) {
    sessionStorage.removeItem(key);
}

function clearSessionStorage() {
    sessionStorage.clear();
}

function writeToConsole(value) {
    console.log(value);
}

function hideElement(id) {
    $("#" + id).hide();
}

