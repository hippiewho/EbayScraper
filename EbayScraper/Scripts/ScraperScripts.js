var index = 0;

var isFloat = function (num) {
    try {
        var n = parseFloat(num);
    } catch (e) {
        return false;
    }
    return true;
}

var beginScrape = function () {
    var term = $("#ScrapeSearchTerm").val();
    if (term.length > 0) {
        $.ajax({
            url: "/Home/Scrape/" + term,
            datatype: "JSON",
            success: function (results) {
                displaysearchresults(results);
            }
        });
    }
};

$(document).ready(function () {
    $("#StartScrapeButton").click(beginScrape);

    $("#ScrapeSearchTerm").on("keypress",
        function(event) {
            if (event.which === 13) {
                $(this).attr("disabled", "disabled");

                beginScrape();

                $(this).removeAttr("disabled");
            }
        });


});


var displaysearchresults = function (results) {
    $("#scraperesultscontainer").html("");

    const jsonobj = JSON.parse(results);
    var list = "";
  

    list = createItemTable(jsonobj, list);
    $("#scraperesultscontainer").append(list);
    calculateAveragePrice();

}

function calculateAveragePrice() {
    var total = 0.00;
    var count = 0;
    for (let i = 0; i < index; i++) {
        if ($(`#checkbox${i}`).is(':checked')) {
            var currprice = $(`#pricetext${i}`).text().substring(1);
            if (isFloat(currprice)) {
                count++;
                total += parseFloat(currprice);
            }
        }
    }
    var average = total / count;
    $("#averagecalc").html("<h3> Average: " + average.toFixed(2) + "</h3>");


}

function createItemTable(jsonobj, list) {
    index = 0;
    list = list + "<table class=\"table\"><thead><tr><th scope=\"col\">#</th><th scope=\"col\">Image:</th><th scope=\"col\">Title</th><th scope=\"col\">Price</th><th>check box</th></tr></thead>";
    for (var obj in jsonobj) {
        var curr = jsonobj[obj];
        list = list + "<tr><td>" + index + "</td><td><img src=\"" +
            curr["ImageUrl"] +
            "\"></td><td id=\"pricetext" + index + "\">" +
            curr["Price"] +
            "</td><td>" +
            curr["Title"] +
            "</td><td><input type=\"checkbox\" class=\"form - check - input\" id=\"checkbox" + index + "\" onclick=\"calculateAveragePrice()\" checked></td></tr>";
        index++;
    }
    list = list + "</tbody></table>";
    return list;
}
