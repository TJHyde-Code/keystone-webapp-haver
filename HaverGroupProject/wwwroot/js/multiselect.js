let DDLforChosen = document.getElementById("selectedOptions");
let DDLforAvail = document.getElementById("availOptions");

function switchOptions(event, senderDDL, receiverDDL) {

    let senderID = senderDDL.id;
    let selectedOptions = document.querySelectorAll(`#${senderID} option:checked`);
    event.preventDefault();

    if (selectedOptions.length === 0) {
        alert("Nothing to move. ");
    }
    else {
        selectedOptions.forEach(function (o, idx) {
            senderDDL.remove(o.index);
            receiverDDL.appendChild(o);
        });
    }
}

let addOptions = (event) => switchOptions(event, DDLforAvail, DDLforChosen);
let removeOptions = (event) => switchOptions(event, DDLforChosen, DDLforAvail);

document.getElementById("btnLeft").addEventListener("click", addOptions);
document.getElementById("btnRight").addEventListener("click", removeOptions);

document.getElementById("btnSubmit").addEventListener("click", function () {
    DDLforChosen.childNodes.forEach(opt => opt.selected = "selected");
})