function updateFoods()
{
    
    $("#suggestions").innerHTML = $.getJSON("https://api.nal.usda.gov/ndb/search/?format=json&q=" + $("#foodForm").innerHTML + "&sort=n&max=25&offset=0&api_key=hyMAaC37dIT57p36cBZ1Sn6tK5XYfnOLP4IaNSs7");
}