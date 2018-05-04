$(document).ready(function() {
    $("#StartScrapeButton").click(function() {
        var term = $("#ScrapeSearchTerm").val();
        console.log("the term: " + term);
        $.ajax({
            url: "/Home/Scrape/" + term,
            datatype: "JSON",
            success: function(results) {
                displaysearchresults(results);
            }
        });
    });
})


var displaysearchresults = function (results) {
    console.log(results);
    $("#scraperesultscontainer").html(results);
}