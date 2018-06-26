$(".category-widget").on('click', 'a', function (e) {
    e.preventDefault();
    var categoryId = $(this).data('category');
    var respond = "";
    var paginationNumber = $(this).data('number');

    $.ajax({
        type: "GET",
        url: "/home/categories/" + categoryId,
        success: function (data, xhr) {
            var x = data.Items;

            $.each(x, function (index, t) {

                respond += `    <div class="post post-row">
                            <a class="post-img" href="/home/show/${t.Id}"><img src="/Content/Images/${t.src}" alt=""></a>
                            <div class="post-body">
                                
                                <h3 class="post-title"><a href="/home/show/${t.Id}">${t.heading}</a></h3>
                                <ul class="post-meta">
                                    <li>${t.username}</li>
                                    <li>${t.time}</li>
                                </ul>
                                <p>${t.paragraph}</p>
                            </div>
                        </div>`;

            });

            paginationNumber = paginationNumber / 3;
            paginationNumber = Math.ceil(paginationNumber);
            // alert(paginationNumber);
            var insertpagination = '<ul class="pagination" id="categoryPagination">';
            if (paginationNumber !== 1) {
                for (var i = 0; i < paginationNumber; i++) {
                    insertpagination += '<li class="page-item"><a class="page-link" data-page="' + i + '" data-categoryId="' + categoryId + '" href="#">' + i + '</a></li>';
                }
            }
            insertpagination += '</ul>';

            $("#posts").html(respond);
            $("#pagination").html(insertpagination);
        },
        error: function (xhr, status, error) {
            alert(xhr.status);
        }
    }).done(function () {
        $('#categoryPagination > li').on('click', 'a', function (e) {
            e.preventDefault();
            var pageId = $(this).data('page');
            var category = $(this).data('categoryid');
            
            var obj = { page: pageId, category: category };
            console.log(obj);
            $.ajax({
                type: "post",
                dataType: "json",
                data: JSON.stringify(obj),
                url: "/home/categoriesfilter",
                contentType: "application/json",
                success: function (data) {
                    var respond = "";
                    var x = data.Items;
                    $.each(x, function (index, t) {

                        respond += `    <div class="post post-row">
                            <a class="post-img" href="/home/show/${t.Id}"><img src="/Content/Images/${t.src}" alt=""></a>
                            <div class="post-body">
                                
                                <h3 class="post-title"><a href="/home/show/${t.Id}">${t.heading}</a></h3>
                                <ul class="post-meta">
                                    <li>${t.username}</li>
                                    <li>${t.time}</li>
                                </ul>
                                <p>${t.paragraph}</p>
                            </div>
                        </div>`;
                    });
                    $("#posts").html(respond);
                },
                error: function (xhr, status, error) {
                    console.log(error);
                }

            });
            //zavrsiti ajax
        });

    });//end of done. and of ajax.

});

$(".pagination").on('click', 'a', function (e) {
    e.preventDefault();
    var page = $(this).data('page');
    //alert(page);

    $.ajax({
        type: "GET",
        url: "Home/pages/" + page,
        success: function (data) {
            console.log(data);
            var respond = "";
            var x = data.Items;
            $.each(x, function (index, t) {

                respond += `    <div class="post post-row">
                            <a class="post-img" href="/home/show/${t.Id}"><img src="/Content/Images/${t.src}" alt=""></a>
                            <div class="post-body">
                                
                                <h3 class="post-title"><a href="/home/show/${t.Id}">${t.heading}</a></h3>
                                <ul class="post-meta">
                                    <li>${t.username}</li>
                                    <li>${t.time}</li>
                                </ul>
                                <p>${t.paragraph}</p>
                            </div>
                        </div>`;
            });
            $("#posts").html(respond);
        },
        error: function (xhr, status, error) {
            console.log(xhr.status);
        },
    })
});





