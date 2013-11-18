$(function () {


    //Listeners
    $('#CalcBtn').on("click", function () {
        GetPath();
    });


    function GetPath() {

        var points = {
            Stage: {
                Height: stage.getHeight(),
                Width: stage.getWidth()
            },
            Origin: {
                X: origin.getX(),
                Y: origin.getY()
            },
            Destination: {
                X: destination.getX(),
                Y: destination.getY()
            },
            Obstacles: [{
                x: box.getX(),
                y: box.getY(),
                Height: box.getHeight(),
                Width: box.getWidth()
            }]
        };

        $.ajax({
            url: 'home/plotpath/',
            data: JSON.stringify(points),
            dataType: 'json',
            contentType: 'application/json',
            type: 'POST',
            success: function (data) {
                renderPoints(data);
            }
        });
    }


    //Grid Stage
    var stage = new Kinetic.Stage({
        container: 'gridCanvas',
        width: 1000,
        height: 700
    });

    var layer = new Kinetic.Layer();

    var width = stage.getWidth();
    var height = stage.getHeight();

    for (var x = 50; x < width; x = x + 50) {
        for (var y = 50; y < height; y = y + 50) {

            var circle = new Kinetic.Circle({
                x: x,
                y: y,
                radius: 1,
                fill: 'black'
            });

            // add the shape to the layer
            layer.add(circle);
        }
    }


    ////////
    //Obstacle Stage

    var boxlayer = new Kinetic.Layer();
    var Id = 0;

    var box = new Kinetic.Rect({
        x: 100,
        y: 100,
        width: 100,
        height: 50,
        fill: '#00D2FF',
        stroke: 'black',
        strokeWidth: 1,
        draggable: true
    });

    box.on('dragend', function (evnt) {

        var localBox = box;

        var xPos = 50 * Math.round(localBox.getX() / 50);
        var yPos = 50 * Math.round(localBox.getY() / 50);

        pathLayer.clear();

        localBox.setX(xPos);
        localBox.setY(yPos);
        boxlayer.draw();

        GetPath();
    });

    box.on('mousedown', function (evnt) {
        box.setStrokeWidth(4);

    });

    window.addEventListener('keydown', doKeyDown, true);

    function doKeyDown(evt) {

        var isShift = evt.shiftKey;

        switch (evt.keyCode) {
            case 38:
                /* Up arrow was pressed */

                var localBox = box;
                pathLayer.clear();

                if (isShift) {
                    if (localBox.getHeight() > 50)
                        localBox.setHeight(localBox.getHeight() - 50);

                    boxlayer.draw();

                } else {

                    localBox.setY(localBox.getY() - 50);
                    boxlayer.draw();
                }

                break;
            case 40:
                /* Down arrow was pressed */

                var localBox = box;
                pathLayer.clear();

                if (isShift) {
                    localBox.setHeight(localBox.getHeight() + 50);
                    boxlayer.draw();

                } else {
                    localBox.setY(localBox.getY() + 50);
                    boxlayer.draw();
                }

                break;
            case 37:
                /* Left arrow was pressed */

                var localBox = box;
                pathLayer.clear();

                if (isShift) {
                    if (localBox.getWidth() > 50)
                        localBox.setWidth(localBox.getWidth() - 50);

                    boxlayer.draw();

                } else {
                    localBox.setX(localBox.getX() - 50);
                    boxlayer.draw();
                }

                break;
            case 39:
                /* Right arrow was pressed */
                var localBox = box;
                pathLayer.clear();

                if (isShift) {
                    localBox.setWidth(localBox.getWidth() + 50);
                    boxlayer.draw();

                } else {
                    localBox.setX(localBox.getX() + 50);
                    boxlayer.draw();
                }

                break;
        }
    }

    //// add cursor styling
    box.on('mouseover', function () {
        document.body.style.cursor = 'pointer';
    });

    box.on('mouseout', function () {
        document.body.style.cursor = 'default';
    });

    var endpointLayer = new Kinetic.Layer();
    var origin = new Kinetic.Circle({
        x: 50,
        y: 50,
        radius: 15,
        fill: 'blue'
    });
    endpointLayer.add(origin);


    var destination = new Kinetic.Circle({
        x: width - 50,
        y: (height / 2),
        radius: 15,
        fill: 'red'
    });

    endpointLayer.add(destination);

    var pathLayer = new Kinetic.Layer();
    var redLine;

    stage.add(endpointLayer);
    stage.add(boxlayer);
    stage.add(layer);
    stage.add(pathLayer);
    boxlayer.add(box);

    stage.add(boxlayer);

    function renderPoints(points) {

        pathLayer.removeChildren();
        redLine = new Kinetic.Line({
            points: points,
            stroke: 'red',
            strokeWidth: 5,
            lineCap: 'round',
            lineJoin: 'round'
        });

        pathLayer.add(redLine);
        pathLayer.draw();
    }
});