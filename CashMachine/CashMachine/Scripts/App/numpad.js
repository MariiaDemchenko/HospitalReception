$(function () {
    var cardNumMaskLength = 16;
    var pinCodeMaskLength = 4;

    var keyCode0NumPad = 96;
    var keyCode9NumPad = 105;
    var keyCode0 = 48;

    var cardNumClassName = "card-num";
    var pinCodeClassName = "pin-code";

    var numAttribute = "num";

    var displayIdAttribute = "displayId";

    var allowedKeys = ["Backspace", "Delete", "ArrowLeft", "ArrowRight"];

    var displayId;
    var displayElementId;

    function setDisplayId(id) {
        displayId = "#" + id;
        displayElementId = id;
    }

    $(".btn-num").on("click", function () {
        event.preventDefault();
        var num = $(this).attr(numAttribute);
        setDisplayId($(this).attr(displayIdAttribute));
        updateInput(num);
        $(displayId).focus();
    });

    $(".btn-clear").on("click", function () {
        setDisplayId($(this).attr(displayIdAttribute));
        var inputField = $(displayId);
        inputField.val("");
        inputField.focus();
        event.preventDefault();
    });

    $(".btn-back").on("click", function (event) {
        event.preventDefault();
        history.go(-1);
    });

    function checkIsSymbolAllowed(symbol) {
        return allowedKeys.includes(symbol);
    }

    function getKeyCode(keyCode) {
        if (keyCode >= keyCode0NumPad && keyCode <= keyCode9NumPad) {
            keyCode -= keyCode0;
        }
        return keyCode;
    }

    function getNumericInput(input) {
        return input.split("-").join("");
    }

    function getMaskLength() {
        var inputElement = $(displayId);
        var isCardNum = inputElement.hasClass(cardNumClassName);
        var isPassword = inputElement.hasClass(pinCodeClassName);

        var maskLength = 0;
        if (isCardNum) {
            maskLength = cardNumMaskLength;
        } else if (isPassword) {
            maskLength = pinCodeMaskLength;
        }
        return maskLength;
    }

    $(".num-display").on("keydown", function (e) {
        setDisplayId($(this).attr("id"));
        var key = e.key;
        var keyCode = getKeyCode(e.keyCode);

        var isAllowedSymbol = checkIsSymbolAllowed(key);
        if (!isAllowedSymbol) {
            var symbol = String.fromCharCode(keyCode);
            if (jQuery.isNumeric(symbol)) {
                updateInput(symbol);
            }
            return false;
        }
        return true;
    });

    function insertSymbol(symbol) {
        var input = document.getElementById(displayElementId);
        var selectionStart = input.selectionStart;
        var selectionEnd = input.selectionEnd;
        var inputValue = input.value;
        var partBegin = inputValue.slice(0, selectionStart);
        var partEnd = inputValue.slice(selectionEnd, inputValue.length);
        var maskLength = getMaskLength();
        var inputNumbers = getNumericInput(partBegin + partEnd);
        if (maskLength !== 0 && inputNumbers.length >= maskLength) {
            return selectionStart;
        }
        input.value = partBegin + symbol + partEnd;
        return selectionStart + 1;
    }

    function formatCardNum(selectionStart) {
        var input = $(displayId);
        var clearInput = getNumericInput(input.val());
        var slices = [];

        if (selectionStart !== 0 && selectionStart % 5 === 0) {
            selectionStart++;
        }

        for (var charsCounter = 0, charsLength = clearInput.length; charsCounter < charsLength; charsCounter += 4) {
            slices.push(clearInput.substring(charsCounter, charsCounter + 4));
        }

        var formattedInput = slices[0];

        for (var sliceCounter = 1; sliceCounter < slices.length; sliceCounter++) {
            formattedInput = formattedInput + "-" + slices[sliceCounter];
        }
        input.val(formattedInput);
        setSelectionStart(selectionStart);
    }

    function setSelectionStart(selectionStart) {
        var input = document.getElementById(displayElementId);
        input.selectionStart = selectionStart;
        input.selectionEnd = selectionStart;
    }

    function updateInput(symbol) {
        var inputField = $(displayId);
        var selectionStart = insertSymbol(symbol);

        $(displayId + ".card-num").one("valueChanged",
            function () {
                formatCardNum(selectionStart);
            });

        $(displayId + ":not('.card-num')").one("valueChanged",
            function () {
                setSelectionStart(selectionStart);
            });

        inputField.trigger("valueChanged");
    }
});