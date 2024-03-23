CKEDITOR.dialog.add('sliderDialog', function (editor) {
    var value = "";
    var gelenveri = "";
    return {
        title: 'Slider Properties',
        minWidth: 340,
        minHeight: 75,
        contents: [
            {
                id: 'slider',
                label: 'Slider',
                elements: [
                    {
                        id: 'sliderTyple',
                        type: 'select',
                        label: 'Slider Type',
                        items: [
                            ['slider 1', '1'],
                            ['slider 2', '2'],
                            ['slider 3', '3'],
                            ['slider 4', '4']
                        ],
                        onChange: function (api) {
                            value = this.getValue();
                            var editorgelen = CKEDITOR.instances.editor1,
                                range = editorgelen.getSelection().getRanges()[0],
                                el = editorgelen.document.createElement('div');
                            el.append(range.cloneContents());
                            gelenveri = el.getHtml();
                        },
                        'default': '1',
                    }
                ]
            }
        ],
        onOk: function () {
                if (gelenveri.length < 50) {
                    editor.insertHtml(
                        '<div data-slider="slider1" style="display: flex; width: 100%; justify-content: space-between;">' +
                        '<div>' +
                        '<img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdVryfA8aqQc_XyopPF2YIGDdZy-HPVEniGS_L313xHsKmX2-6lBy0iYS7D_2G3YAHoH4&usqp=CAU" alt="">' +
                        '</div>' +
                        '<div>' +
                        '<img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdVryfA8aqQc_XyopPF2YIGDdZy-HPVEniGS_L313xHsKmX2-6lBy0iYS7D_2G3YAHoH4&usqp=CAU" alt="">' +
                        '</div>' +
                        '<div>' +
                        '<img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdVryfA8aqQc_XyopPF2YIGDdZy-HPVEniGS_L313xHsKmX2-6lBy0iYS7D_2G3YAHoH4&usqp=CAU" alt="">' +
                        '</div>' +
                        '<div>' +
                        '<img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdVryfA8aqQc_XyopPF2YIGDdZy-HPVEniGS_L313xHsKmX2-6lBy0iYS7D_2G3YAHoH4&usqp=CAU" alt="">' +
                        '</div>' +
                        '</div>'
                    )
                } else {
                    editor.insertHtml(gelenveri.replace('data-slider="slider1"', 'data-slider="slider' + value + '"').replace('data-slider="slider2"', 'data-slider="slider' + value + '"').replace('data-slider="slider3"', 'data-slider="slider' + value + '"').replace('data-slider="slider4"', 'data-slider="slider' + value + '"').replace('data-slider="slider5"', 'data-slider="slider' + value + '"').replace('data-slider="slider6"', 'data-slider="slider' + value + '"').replace('data-slider="slider7"', 'data-slider="slider' + value + '"'))
                }
        }
    };
});