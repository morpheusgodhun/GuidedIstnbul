CKEDITOR.plugins.add( 'slider', {
    icons: 'slider',
    init: function( editor ) {
        editor.addCommand( 'slider', new CKEDITOR.dialogCommand( 'sliderDialog' ) );
        /*
        editor.addCommand( 'timestamp', {
            exec: function( editor ) {
                var now = new Date();
                editor.insertHtml( 'The current date and time is: <em>' + now.toString() + '</em>' 
                ),
                new CKEDITOR.dialogCommand( 'timestampDialog' );
            }
        });
        */
        editor.ui.addButton( 'Slider', {
            label: 'Insert Slider',
            command: 'slider',
            toolbar: 'insert'
        });

        CKEDITOR.dialog.add( 'sliderDialog', this.path + 'dialogs/slider.js' );
    }
});