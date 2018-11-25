$(document).ready(function () {
    disableCheckboxes(true);

    $(".account").click(function () {
        $(".selected").removeClass("selected");
        $(this).addClass("selected");
        disableCheckboxes(false);
    });

    $(".posted-checkbox").click(function () {
        var checked = $(this).is(':checked');
        postForAccount($(".selected").attr("id"), $(this).attr("id"), checked);
    });
});

function disableCheckboxes(disabled) {
    $(".posted-checkbox").attr("disabled", disabled);
}

function postForAccount(accountId, copyId, checkedState) {
    // call ajax 
    console.log("copyId: " + copyId + " accountId: " + accountId);

    $.ajax({
        type: "POST",
        url: "/Copy/MarkPosted",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("RequestVerificationToken",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {
            'copyId': copyId,
            'accountId': accountId,
            'checkedState': checkedState
        },
        success: function (response) {
            console.log("success");
        },
        failure: function (response) {
            console.log("failure");
        }
    });

}