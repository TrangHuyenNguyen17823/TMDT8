using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Ecommerce_Client.Models; // Assuming you have a Product model

namespace Ecommerce_Client
{
    public partial class ChiTietSanPham : System.Web.UI.Page
    {
        private ChiTietSanPhamDataUtils dataUtils = new ChiTietSanPhamDataUtils(); // Instantiate data access utils

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve productId from query string or other sources (e.g., session)
                if (Request.QueryString["productId"] != null)
                {
                    int productId = Convert.ToInt32(Request.QueryString["productId"]);

                    // Get product details by productId
                    Product product = dataUtils.GetProductById(productId);

                    if (product != null)
                    {
                        // Display product details on the page
                        lblProductName.Text = product.name;
                        lblProductPrice.Text = string.Format("{0:C}", product.price);
                        lblProductStock.Text = "In Stock: " + product.stock.ToString();
                        imgProduct.ImageUrl = product.image;

                        // Optionally, you can bind the product details to other controls or update UI accordingly
                    }
                    else
                    {
                        // Handle case where product is not found
                        lblProductName.Text = "Product not found";
                        // Optionally, hide other product details or show an error message
                    }

                    // Get related products (assuming you have a placeholder control for related products)
                    List<Product> relatedProducts = dataUtils.GetRelatedProducts(product.categoryId, productId, 5);
                    rptRelatedProducts.DataSource = relatedProducts;
                    rptRelatedProducts.DataBind();
                }
                else
                {
                    // Handle case where productId is not provided in query string
                    // Optionally, redirect or display an error message
                }
            }
        }

        // Event handler for Add to Cart button click (if needed)
        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            // Implement logic to add product to cart
            // Example: Redirect to cart page or show a confirmation message
        }

        // Event handler for clicking on related product (if needed)
        protected void rptRelatedProducts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                int productId = Convert.ToInt32(e.CommandArgument);
                // Redirect to the detail page of the related product (ChiTietSanPham.aspx?productId=productId)
                Response.Redirect("ChiTietSanPham.aspx?productId=" + productId);
            }
        }
    }
}
