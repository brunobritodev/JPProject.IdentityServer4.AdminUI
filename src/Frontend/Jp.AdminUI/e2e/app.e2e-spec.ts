import { JpProjectWebAppPage } from "./app.po";
import { TestBed } from "@angular/core/testing";
import { HttpClientModule } from "@angular/common/http";

describe("jpproject WebApp", function () {
  let page: JpProjectWebAppPage;

  beforeEach(() => {
    page = new JpProjectWebAppPage();
  });

  it("should display sign-in page", () => {
    page.navigateTo();
    expect(page.getButtonText()).toEqual("Login");
    expect(page.getUrl()).toContain("/login");
  });
});
