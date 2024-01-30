 'use strict';
   

        //As an example, in normal JavaScript, mistyping a variable name creates a new global variable. In strict mode, this will throw an error, making it impossible to accidentally create a global variable.
       // In normal JavaScript, a developer will not receive any error feedback assigning values to non - writable properties.
       // In strict mode, any assignment to a non - writable property, a getter - only property, a non - existing property, a non - existing variable, or a non - existing object, will throw an error.

       
        //function x(p1, p1) { };


        const modal = document.querySelector('.modal-container.hidden');
        const overlay = document.querySelector('.overlay.hidden');
        const btns = document.querySelectorAll('.button');
        const modalFirstName = document.getElementById('modalFirstName');
        const modalLastName = document.getElementById('modalLastName');
        const firstNameInput = document.getElementById('firstName');
        const lastNameInput = document.getElementById('lastName');
        const firstNameMsg = document.getElementById('firstNameMsg');
        const lastNameMsg = document.getElementById('lastNameMsg');
        const cancelBtn = document.querySelector('.buttonCancel');
        const saveBtn = document.querySelector('.buttonSave');
        var message = document.getElementById("modalMessage");


class ModalCl {
    constructor() {
        this.modal = document.querySelector('.modal-container.hidden');
        this.overlay = document.querySelector('.overlay.hidden');
        this.cancelBtn = document.querySelector('.buttonCancel');
        this.saveBtn = document.querySelector('.buttonSave');
        this.message = document.getElementById("modalMessage");
        this.cancelBtn.addEventListener('click', () => this.Close());
        this.overlay.addEventListener('click', () => this.Close());
        this.saveBtn.addEventListener('click', () => this.Save());
    }

    Close() {
        this.modal.style.display = "none";
        this.overlay.style.display = "none";
        console.log('hiddenadded');
    }

    Open() {
        this.modal.style.display = "block";
        this.overlay.style.display = "block";
        console.log('hiddenremoved');
    }

    Save() {
        const newFirstName = modalFirstName.value;
        const newLastName = modalLastName.value;
        const abstractId = this.modal.getAttribute('data-abstract-id');
        const row = this.modal.getAttribute('data-row');
        updateDatabase(abstractId, newFirstName, newLastName, row, this);
    }
}


            // // Abstract Identifiers: This approach involves using substitute identifiers to represent real IDs, especially in client - side and front - end development.It helps obfuscate sensitive information from being directly exposed to the UI and potentially to end - users.
            // // Obfuscation: Obfuscation entails intentionally making code or data difficult to understand.It's commonly used to protect software from reverse engineering or to conceal its logic and algorithms. This technique involves altering the code's structure or using complex transformations to
            //  make it harder for someone to comprehend or modify.

function updateGrid(abstractId, firstName, lastName, rowIndex) {
    const existingRow = document.querySelector(`[data-abstract-id="${abstractId}"]`);
    console.log(abstractId);
    if (existingRow) {
        console.log(`Updating existing row with abstractId: ${abstractId}`);
        existingRow.cells[0].textContent = firstName;
        existingRow.cells[1].textContent = lastName;
    } else {
        console.log(`Inserting new row with abstractId: ${abstractId}`);
        const table = document.getElementById('table');
        const newRow = table.insertRow(-1);
        const rowId = 'row-' + uuidv4();
        newRow.setAttribute('data-id', rowId);
        newRow.setAttribute('data-abstract-id', abstractId);

        const cell2 = newRow.insertCell(0);
        cell2.appendChild(document.createTextNode(firstName));

        const cell3 = newRow.insertCell(1);
        cell3.appendChild(document.createTextNode(lastName));

        const cell4 = newRow.insertCell(2);

        const deleteButton = document.createElement('button');
        deleteButton.className = 'btn btn-danger';
        deleteButton.innerHTML = '<i class="bi bi-trash"></i>';
        deleteButton.onclick = function () {
            handleDeleteButtonClick(this);
        };
        cell4.appendChild(deleteButton);

        const editButton = document.createElement('button');
        editButton.className = 'btn btn-primary';
        editButton.innerHTML = '<i class="bi bi-pencil"></i>';
        editButton.onclick = function () {
            handleEditClick(event, rowIndex);
        };
        cell4.appendChild(editButton);
    }
}


        //----------------------------------loadInitialData----------
       
        function loadInitialData() {
            $.ajax({
                url: `http://localhost:5271/Person/GetPersonByIdJson`,
                type: 'GET',
                success: function (response) {
                    response.forEach(function (person) {
                        updateGrid(person.abstractId, person.firstName, person.lastName);
                    });
                    console.log('Initial data loaded successfully');
                },
                error: function (xhr, status, err) {
                    console.error('Failed to load initial data', err);
                }
            });
        }

        document.addEventListener('DOMContentLoaded', function () {
            loadInitialData();
        });


        function sortTable(columnIndex) {
            var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
            table = document.getElementById("table");
            if (!table) {
                console.error("Table not found.");
                return; // Stop the function if the table isn't found.
            }
            switching = true;
            dir = "asc"; // Initially set the direction to ascending

            // Perform the sorting loop until no more switching is needed
            while (switching) {
                switching = false;
                rows = table.rows;

                // Loop through all table rows (except the first, which contains table headers)
                for (i = 1; i < (rows.length - 1); i++) {
                    shouldSwitch = false;
                    // Get the two elements to compare, one from current row and one from the next
                    x = rows[i].getElementsByTagName("TD")[columnIndex];
                    y = rows[i + 1].getElementsByTagName("TD")[columnIndex];

                    if (!x || !y) {
                        console.error("TD not found.");
                        return; // Stop the function if the expected TD isn't found.
                    }

                    // Check if the two rows should switch place, depending on the direction
                    if ((dir == "asc" && x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) ||
                        (dir == "desc" && x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase())) {
                        shouldSwitch = true;
                        break;
                    }
                }

                // If a switch has been marked, perform it and mark that a switch has been done
                if (shouldSwitch) {
                    rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
                    switching = true;
                    switchcount++; // This is a counter for how many switches have been done
                } else {
                    // If no switching has been done and the direction was "asc",
                    // set the direction to "desc" and run the while loop again.
                    if (switchcount == 0 && dir == "asc") {
                        dir = "desc";
                        switching = true;
                    }
                }
            }

            // Update sort arrows for all columns
            var arrows = table.querySelectorAll('.sort-arrow');
            arrows.forEach(function (arrow) { arrow.textContent = '↑↓'; });

            // Update the current sort arrow
            var currentArrow = table.rows[0].cells[columnIndex].querySelector('.sort-arrow');
            if (currentArrow) {
                currentArrow.textContent = dir === 'asc' ? '↓' : '↑';
            } else {
                console.error("Sort arrow not found.");
                return; // Stop the function if the sort arrow isn't found.
            }
        }
        /////////////////////////////////////////////////////////////

        function handleSaveButtonClick() {
            const personForm = new PersonFormCl();
           
            const firstName = firstNameInput.value.trim();
            const lastName = lastNameInput.value.trim();
            personForm.formValidation = { firstName, lastName };
            if (!personForm.firstNameMsg.textContent && !personForm.lastNameMsg.textContent) {
                saveToDatabase(firstName, lastName);
                firstNameInput.value = "";
                lastNameInput.value = "";
            }
        }

    class PersonFormCl {

       constructor()
       {
        this.firstNameInput = document.getElementById('firstName');
        this.lastNameInput = document.getElementById('lastName');
        this.firstNameMsg = document.getElementById('firstNameMsg');
        this.lastNameMsg = document.getElementById('lastNameMsg');
       }

        set formValidation(names) {
            const { firstName, lastName } = names;

            if (!this.firstNameInput.value.trim()) {
                this.firstNameMsg.textContent = "First name is required";
            } else {
                this.firstNameMsg.textContent = "";
            }

            if (!this.lastNameInput.value.trim()) {
                this.lastNameMsg.textContent = "Last name is required";
            } else {
                this.lastNameMsg.textContent = "";
            }

            if (/\d/.test(firstName)) {
                this.firstNameMsg.textContent = "First name cannot contain numbers";
            }

            if (/\d/.test(lastName)) {
                this.lastNameMsg.textContent = "Last name cannot contain numbers";
            } 
        }
}


        function saveToDatabase(firstName, lastName, rowIndex) {
            $.ajax({
                url: `http://localhost:5271/Person/Create`,
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ FirstName: firstName, LastName: lastName, RowIndex: rowIndex }),
                success: function (response) {
                    updateGrid(response.abstractId, firstName, lastName);
                    showMessage(`Saved successfully: ${firstName} ${lastName}`, 'success');
                },
                error: function (xhr) {
                    const errorMessage = xhr.responseJSON?.message || 'Failed to save data';
                    showMessage(errorMessage, 'error');
                }
            });
        }

        function displayNotification(message, type) {
            const notificationContainer = document.getElementById('notificationContainer');
            const notificationMessage = document.createElement('div');
            notificationMessage.classList.add('notification-message');

            if (type === 'success') {
                notificationMessage.classList.add('success');
            } else if (type === 'error') {
                notificationMessage.classList.add('error');
            }

            notificationMessage.textContent = message;
            notificationContainer.appendChild(notificationMessage);

            setTimeout(() => notificationContainer.removeChild(notificationMessage), 3000);
        }
        function showMessage(text, type) {
            displayNotification(text, type);
        }



       
        //--------------------------------Delete---------------------------------------------------------
         function deleteFromDatabase(abstractId, row) {
            $.ajax({
                url: `http://localhost:5271/Person/DeleteConfirmed/${abstractId}`,
                type: 'POST',
                contentType: 'application/json', 
                data: JSON.stringify({ AbstractId: abstractId }),
                success: function (data) {
                    row.remove();
                    displayNotification("Data deleted successfully", "success"); 
                    console.log("Data deleted successfully");
                    // var newUrl = "http://localhost:5214/person";

                    // history.pushState(null, null, newUrl);
                },
                error: function (xhr, status, error) {
                    displayNotification("Error deleting data", "error"); 
                    console.error("Error:", error);
                    console.log(id);
                    console.log(row);
                }
            });
        }



         function handleDeleteButtonClick(buttonElement) {
    

            const row = buttonElement.closest('tr');
            const rowId = 'row-' + uuidv4();
            const id = row.getAttribute('data-id');
            const abstractId = row.getAttribute('data-abstract-id');
            const hidden = row.cells[0].textContent;
            const firstName = row.cells[1].textContent;
            const lastName = row.cells[2].textContent;
            console.log(hidden);
            console.log(id);
            console.log(abstractId);
            console.log(firstName);
            console.log(lastName);
            if (confirm(`Are you sure you want to delete <${hidden}${firstName}${lastName}> ?😊`)) {
                deleteFromDatabase(abstractId, row)
                console.log("Data deleted");
            } else {
                console.log("You pressed Cancel!");
            }

        }

        const table = document.getElementById('table');
table.addEventListener('click', function (event) {
    if (event.target && event.target.classList.contains('bi-trash')) {
        handleDeleteButtonClick(event.target);
        console.log("trash click");

    }
}, { once: true });
        //--------------------------------Edit---------------------------------------------------------
function handleEditClick(event, rowIndex) {
    const row = event.target.closest('tr');
    if (row) {
        const rowId = row.getAttribute('data-id');
        const abstractId = row.getAttribute('data-abstract-id');
        const currentFirstName = row.cells[0].textContent;
        const currentLastName = row.cells[1].textContent;
        console.log(rowId);
        console.log(abstractId);

        const modal = new ModalCl();
        modal.Open();
        modal.setFieldValues(currentFirstName, currentLastName);
        modal.setMessage("Edit the fields you want to update.");

        modal.setSaveCallback((newFirstName, newLastName) => {
            updateDatabase(abstractId, newFirstName, newLastName, row, modal);
        });
    }
}

function handleEditClick(event, rowIndex,modal) {
    const row = event.target.closest('tr');
    if (row) {
        const rowId = row.getAttribute('data-id');
        const abstractId = row.getAttribute('data-abstract-id');
        const currentFirstName = row.cells[0].textContent;
        const currentLastName = row.cells[1].textContent;
        console.log(rowId);
        console.log(abstractId);
      
        modal.Open();
        modalFirstName.value = currentFirstName;
        modalLastName.value = currentLastName;
        message.textContent = "Edit the fields you want to update.";

        //const saveButton = document.getElementById('saveButton');
        //console.log(abstractId);
        //saveButton.onclick = function () {
        //const newFirstName = modalFirstName.value;
        //console.log(newFirstName);
        //const newLastName = modalLastName.value;
        //updateDatabase(abstractId, newFirstName, newLastName, row);
        //console.log(abstractId);

        };
    }

              
function updateDatabase(abstractId, newFirstName, newLastName, row,modal) {
    $.ajax({
        url: `http://localhost:5271/Person/Edit/${abstractId}`,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ AbstractId: abstractId, FirstName: newFirstName, LastName: newLastName, Row: row }),

        success: function (response) {
            updateGrid(response.abstractId, response.firstName, response.lastName, row);
            displayNotification("Data Edited successfully", "success"); // Show success notification
            console.log('Data updated successfully');
            const modal = new ModalCl();
            modal.Close();
        },
        error: function (xhr, status, err) {
            console.error('Failed to Edit data', err);
            displayNotification("Error Editing data", "error"); // Show error notification
        }
    });
}
   document.addEventListener('click', function (event) {
    if (event.target && event.target.classList.contains('bi-pencil')) {
        const rowIndex = event.target.closest('tr').rowIndex;
        const modalInstance = new ModalCl(); // Create a new instance here
        handleEditClick(event, rowIndex, modalInstance);
    }
   });


