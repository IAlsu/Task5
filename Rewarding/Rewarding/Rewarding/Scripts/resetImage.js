function clearForm(ctrlId) {
    var old = document.getElementById(ctrlId);
    var newElm = document.createElement('input');
    newElm.type = "file";
    newElm.id = ctrlId;
    newElm.name = old.name;
    newElm.className = old.className;
    old.parentNode.replaceChild(newElm, old);
}