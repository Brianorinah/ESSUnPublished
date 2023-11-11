<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPass.aspx.cs" Inherits="HRPortal.ForgotPass" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8">
	<meta name="author" content="Brian Orina">
	<meta name="viewport" content="width=device-width,initial-scale=1">
	<meta name="description" content="Rest Password">
	<title>Reset Password</title>
	<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-BmbxuPwQa2lc/FVzBcNJ7UAyJxM6wuqIj61tLrc4wSX0szH/Ev+nYRRuWlolflfl" crossorigin="anonymous">
</head>

<body>
    <form id="form1" runat="server">
	<section class="h-100">
		<div class="container h-100">
			<div class="row justify-content-sm-center h-100">
				<div class="col-xxl-4 col-xl-5 col-lg-5 col-md-7 col-sm-9">
					<div class="text-center my-2">
						<img class="mb-4" src="../../siteAssets/Images/logo.jpg" alt="" width="140" height="90">
						<h3>Boskovic Air Charters</h3>
					</div>
					<div class="card shadow-lg">
						<div class="card-body p-5">
							<center><h1 class="fs-4 card-title fw-bold mb-4">Forgot Password</h1></center>							

								<div class="mb-3">
									<label class="mb-2 text-muted" for="">Email Address</label>
                                    <asp:Textbox runat="server" type="email" id="email" name="email" value="" placeholder="Email Address" cssClass="form-control" required autofocus/>
									<%--<input id="email" type="email" class="form-control" name="email" value="" required autofocus>--%>
									
									<div class="invalid-feedback">
										Email is invalid
									</div>
								</div>

								<div class="d-flex align-items-center">
									<button type="submit" class="btn btn-primary ms-auto">
										Send Link
									</button>
								</div>							
							
						</div>
						<div class="card-footer py-3 border-0">
							<div class="text-center">
								Remember your password? <a href="Login.aspx" class="text-dark">Login</a>
							</div>
						</div>
					</div>
					<div class="text-center mt-5 text-muted">
						Copyright &copy; 2017-2021 &mdash; Your Company
					</div>
				</div>
			</div>
		</div>
	</section>
</form>

	<script src="js/login.js"></script>
</body>
</html>
