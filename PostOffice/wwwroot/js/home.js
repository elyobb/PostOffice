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

    $('#modalTag').on('show.bs.modal', function (e) {

        //get data-id attribute of the clicked element
        var copyId = $(e.relatedTarget).data('copy-id');
        $("#copyId").text(copyId);

    });

    $('#createTag').click(function () {
        createTag(parseInt($("#copyId").text()), $("#tagLabelInput").val());
    })
});

function disableCheckboxes(disabled) {
    $(".posted-checkbox").attr("disabled", disabled);
}

function createTag(copyId, tagLabel) {
    $.ajax({
        type: "POST",
        url: "/Tags/CreateTag",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("RequestVerificationToken",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        dataType: 'json',
        data: {
            'copyId': copyId,
            'label':tagLabel
        },
        success: function (response) {
            addTag(copyId, response.tagId, response.label);
        },
        failure: function (response) {
            console.log("failure");
        }
    }); 
    $('#modalTag').modal('hide');
}

function addTag(copyId, tagId, label) {
    var tagListId = "#tagList" + copyId;
    $(tagListId).append('<span class="tag" id="tag' + tagId + '">' + label + '</span>');
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