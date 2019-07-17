<div id="scripto">

<?php echo $this->get_navigation(); ?>

<p>You may log in to access your account and enable certain Scripto features. 
Login may not be required by the administrator.</p>

<?php if ( $this->get_message() ): ?>
<p><?php echo $this->get_message(); ?></p>
<?php endif; ?>

	<div class="row">
		<div class="six columns">
			<form action="" method="post">
				<p>
					<label>Username</label><input type="text" class="u-full-width" name="scripto_username" />
					<label>Password</label><input type="password" class="u-full-width" name="scripto_password" /><br/>
					<input type="submit" class="button-primary" name="scripto_submit_login" value="Login" />
					<a onclick="window.open('<?php echo Scripto_Plugin::get_setting('create_user_url'); ?>', '', 'width=570,height=614')" class="button">Click here to create an account</a>
				</p>
			</form>
		</div>
	</div>

</div>