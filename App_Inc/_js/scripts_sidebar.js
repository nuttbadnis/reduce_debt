
function scroll_to(clicked_link, nav_height) {
	var element_class = clicked_link.attr('href').replace('#', '.');
	var scroll_to = 0;
	if(element_class != '.top-content') {
		element_class += '-container';
		scroll_to = $(element_class).offset().top - nav_height;
	}
	if($(window).scrollTop() != scroll_to) {
		$('html, body').stop().animate({scrollTop: scroll_to}, 1000);
	}
}
// function small_sidebar(){
// 	$('.sidebar').removeClass('active');
// 	$('.sidebar-sm').addClass('active');
// 	$('.overlay').removeClass('active');
// 	$('.body-sidebar').addClass('active');
// 	return false;
// };


jQuery(document).ready(function() {
	
	/*
	    Sidebar
	*/
	$('.dismiss, .overlay').on('click', function() {
		$('.sidebar').removeClass('active');
		$('.sidebar-sm').addClass('active');
        $('.overlay').removeClass('active');
        $('.body-sidebar').addClass('active');
    });

    $('.open-menu').on('click', function(e) {
    	e.preventDefault();
		$('.sidebar').addClass('active');
		$('.sidebar-sm').removeClass('active');
        $('.overlay').addClass('active');
        $('.body-sidebar').removeClass('active');
        // close opened sub-menus
        $('.collapse.show').toggleClass('show');
        $('a[aria-expanded=true]').attr('aria-expanded', 'false');
    });

	/* replace the default browser scrollbar in the sidebar, in case the sidebar menu has a height that is bigger than the viewport */
	//$('.sidebar').mCustomScrollbar({
	//	theme: "minimal-dark"
	//});
	
	/*
	    Navigation
	*/
	$('a.scroll-link').on('click', function(e) {
		e.preventDefault();
		scroll_to($(this), 0);
	});
	
	$('.to-top a').on('click', function(e) {
		e.preventDefault();
		if($(window).scrollTop() != 0) {
			$('html, body').stop().animate({scrollTop: 0}, 1000);
		}
	});
	
});
