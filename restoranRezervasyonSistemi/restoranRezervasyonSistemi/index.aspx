<%@ Page Title="" Language="C#" MasterPageFile="~/restoranRezervasyonSistemi.master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="restoranRezervasyonSistemi.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Restoran Rezervasyon Sistemi</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">

    <form runat="server">
        <div class="row pl-3">
            <div class="col-lg-2 p-3">
                <div class="row">
                    <div class="col-lg-12 col-6">
                        <asp:TextBox required runat="server" cssClass="form-control mb-3" id="masaNo" placeholder="Masa No"></asp:TextBox>
                        <asp:TextBox required runat="server" cssClass="form-control mb-3" id="musteriAdi" placeholder="Müşteri Adı"></asp:TextBox>
                        <asp:TextBox required runat="server" cssClass="form-control mb-3" id="musteriSoyAdi" placeholder="Müşteri Soyadı"></asp:TextBox>
                        <asp:TextBox required runat="server" cssClass="form-control mb-3" id="musteriTelefon" placeholder="Müşteri Telefon No."></asp:TextBox>
                        <asp:Button runat="server" cssClass="btn btn-primary" Text="Rezerve Et" OnClick="rezervasyonEkle" />
                    </div>
                    <div class="col-lg-12 col-6">
                        <h5 class="text-center">Masa Düzeni</h5>
                        <div class="row">
                        <div id="kucukMasalar" runat="server" class="col-5 pt-2 border border-primary "></div>
                        <div class="col-2 p-0 text-center pt-5">
                            <i style=" writing-mode: vertical-rl;text-orientation: upright;"><b>KORİDOR</b></i>
                        </div>
                        <div id="buyukMasalar" runat="server" class="col-5 pt-2 border border-primary "></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-10">
                <div class="container-fluid mt-3 ">

                    <!-- Section: Block Content -->
                    <section>

                        <style>
                            .footer-hover {
                                background-color: rgba(0, 0, 0, 0.1);
                                -webkit-transition: all .3s ease-in-out;
                                transition: all .3s ease-in-out
                            }

                                .footer-hover:hover {
                                    background-color: rgba(0, 0, 0, 0.2)
                                }

                            .text-black-40 {
                                color: rgba(0, 0, 0, 0.4)
                            }
                        </style>
                        <div class="row" id="masalar" runat="server">
                        </div>
                    </section>
                    <!-- Section: Block Content -->

                </div>
            </div>
        </div>
    </form>
</asp:Content>


