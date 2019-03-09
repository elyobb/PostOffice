$(document).ready(function () {
    trimWhitespace();
    disableCheckboxes(true);

    $(".account").click(function () {
        $(".selected").removeClass("selected");
        $(".posted-checkbox").prop("checked", false);
        $(".checked").removeClass("checked unchecked").addClass("unchecked");
        $(this).addClass("selected");
        disableCheckboxes(false);
        retrievePostedCopy($(this).attr('id'));
        $("#hide-posted").click();
        showOrHidePosts();      
    });

    $(".posted-checkbox").click(function () {
        var checked = $(this).is(':checked');
        postForAccount($(".selected").attr("id"), $(this).attr("id"), checked);
        if (checked) {
            $(this).closest("li").removeClass("unchecked");
            if (!$(this).closest("li").hasClass("checked")) {
                $(this).closest("li").addClass("checked");
            }
        }
        else {
            $(this).closest("li").removeClass("checked");
            if (!$(this).closest("li").hasClass("unchecked")) {
                $(this).closest("li").addClass("unchecked");
            }
        }        
        showOrHidePosts();
    });

    $("#show-posted").click(function () {
        $(".checked").show();
    }); 

    $("#hide-posted").click(function () {
        $(".checked").hide();
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
    $("#" + copyId + ".posted-checkbox").closest("li").removeClass("unchecked checked");
    $("#" + copyId + ".posted-checkbox").closest("li").addClass("checked");
    showOrHidePosts();
}

function showOrHidePosts() {
    $("li.unchecked").show();
    var checked = $(".checked");
    $("li.checked").hide();
}

function filter() {
    $(".filterable").each(function () {
        if ($(this).text().indexOf($("#filter").val()) !== -1) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });

}

function trimWhitespace() {
    $(".multi-line").each(function () {
        var text = $(this).text();
        $(this).text($.trim(text));
    });
}