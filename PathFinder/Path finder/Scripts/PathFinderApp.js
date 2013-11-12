$(function () {
    var stage = new Kinetic.Stage({
        container: 'gridCanvas',
        width: 1000,
        height: 700
    });

    var layer = new Kinetic.Layer();

    var width = stage.getWidth();
    var height = stage.getHeight();

    alert(width);
    for (var x = 0; x < width; x + 50) {
       
        console.log(x);
    }
    
    stage.add(layer);



});