$(document).ready(function () {
    //Search
    let hidden = $("#hidden").val().trim();
    if (hidden == "home") {
       
        $(document).on("keyup", "#input-search", function () {
            let search = $(this).val().trim();
            $("#searchList a").remove();
            $("#searchList li").remove();
            if (search.length != 0) {
                $.ajax({
                    url: "/Search/HomeSearch?search=" + search,
                    type: "Get",
                    success: function (res) {
                      $("#searchList").append(res);
                      
                    }
                })
            }
        })
    }
    else {

        $(document).on("keyup", "#input-search", function () {
            let search = $(this).val().trim();
            $("#searchList a").remove();
            $("#searchList li").remove();
            if (search.length != 0) {
                $.ajax({
                    url: "/Search/Search?search=" + search + "&hidden=" + hidden,
                    type: "Get",
                    success: function (res) {
                        if (hidden == "course") {
                            $("#searchList").append(res);
                        }
                        else if (hidden == "event") {
                            $("#searchList").append(res);
                        }
                        else if (hidden == "blog") {
                            $("#searchList").append(res);
                        }
                        else if (hidden == "maincategory") {
                            $("#searchList").append(res);
                        }
                        else if (hidden == "contact") {
                            $("#searchList").append(res);
                        }
                    }
                })
            }
        })
    }
})