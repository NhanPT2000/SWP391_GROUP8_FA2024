function showFacilityInfo() {
    const dropdown = document.getElementById("facilityDropdown");
    const selectedValue = dropdown.value;
    const facilityInfo = document.getElementById("facilityInfo");
    const facilityImage = document.getElementById("facilityImage");
    const facilityName = document.getElementById("facilityName");
    const facilityAddress = document.getElementById("facilityAddress");
    const facilityPhone = document.getElementById("facilityPhone");

    if (selectedValue) {
        const facility = facilities.find(f => f.id == selectedValue);
        facilityImage.src = facility.facilityImage;
        facilityName.textContent = facility.facilityName;
        facilityAddress.textContent = `Address: ${facility.facilityAddress}`;
        facilityPhone.textContent = `Phone: ${facility.facilityPhone}`;
        facilityInfo.style.display = "block";
    } else {
        facilityInfo.style.display = "none";
    }
}