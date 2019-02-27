$(document).ready(function () {
    disableCheckboxes(true);

    $(".account").click(function () {
        $(".selected").removeClass("selected");
        $(".posted-checkbox").prop("checked", false);
        $(this).addClass("selected");
        disableCheckboxes(false);
        var postedCopyList = retrievePostedCopy($(this).attr('id'));
        showOrHidePosts();
    });

    $(".posted-checkbox").click(function () {
        var checked = $(this).is(':checked');
        postForAccount($(".selected").attr("id"), $(this).attr("id"), checked);
        showOrHidePosts();
    });

    $("#show-posted").click(function () {
        $("li").each(function () {
            $(this).show();
        });
    });
});

function disableCheckboxes(disabled) {
    $(".posted-checkbox").attr("disabled", disabled);
}

function postForAccount(accountId, copyId, checkedState) {
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

function retrievePostedCopy(accountId) {
    $.getJSON("/AccountActivities/GetAccountActivityList", { accountId: accountId },
        function (postedCopy) {
            for (var i = 0; i < postedCopy.length; i++) {
                markPosted(postedCopy[i].copyId);
            }

        }
    );
}

function markPosted(copyId) {
    $("#" + copyId + ".posted-checkbox").prop("checked", true);
    $("#" + copyId + ".posted-checkbox").closest("li").hide();
}

function showOrHidePosts() {
    $("div.post-item").each(function () {
        var chks = $(this).find("input:checkbox");
        console.log(chks);
        var numChecked = 0;
        var element = $(this);
        chks.each(function () {
            if ($(this).prop('checked')) {
                numChecked++;
            }
        });
        if (numChecked === chks.length) {
            $(element).hide();
        }
        else {
            $(element).show();
        }
    });
}