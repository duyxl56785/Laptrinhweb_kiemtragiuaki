using System.Globalization;

namespace kiemtragiuaki.Localization
{
    // English / Vietnamese UI strings. Key -> (en, vi).
    public static class Translations
    {
        public const string English = "en-US";
        public const string Vietnamese = "vi-VN";

        private static readonly Dictionary<string, (string En, string Vi)> Strings = new()
        {
            // Layout / menu
            ["AppName"] = ("Dish Management", "Quản lý món ăn"),
            ["Menu_Dishes"] = ("Dishes", "Món ăn"),
            ["Menu_Categories"] = ("Categories", "Loại món ăn"),
            ["Language"] = ("Language", "Ngôn ngữ"),
            ["Hero_Kicker"] = ("Vietnamese kitchen · Menu admin", "Bếp Việt · Quản trị thực đơn"),
            ["Hero_Subtitle"] = ("Search, filter and manage every dish on the menu.", "Tra cứu, lọc và quản lý mọi món ăn trong thực đơn."),
            ["Stat_Total"] = ("dishes", "món ăn"),
            ["Stat_Available"] = ("available", "đang bán"),
            ["Stat_Categories"] = ("categories", "loại món"),
            ["Filter_Title"] = ("Find a dish", "Tìm món"),
            ["Gallery_Hint"] = ("The starred photo is shown on the menu.", "Ảnh có dấu sao được hiển thị trên thực đơn."),

            // Titles
            ["Title_DishList"] = ("Dish list", "Danh sách món ăn"),
            ["Title_DishCreate"] = ("Create dish", "Thêm món ăn"),
            ["Title_DishEdit"] = ("Edit dish", "Sửa món ăn"),
            ["Title_DishDetails"] = ("Dish details", "Chi tiết món ăn"),
            ["Title_DishDelete"] = ("Delete dish", "Xóa món ăn"),
            ["Title_CategoryList"] = ("Dish categories", "Loại món ăn"),
            ["Title_CategoryCreate"] = ("Create category", "Thêm loại món ăn"),
            ["Title_CategoryEdit"] = ("Edit category", "Sửa loại món ăn"),
            ["Title_CategoryDelete"] = ("Delete category", "Xóa loại món ăn"),
            ["Title_Images"] = ("Images", "Hình ảnh"),
            ["Title_AddImage"] = ("Add image", "Thêm ảnh"),
            ["Title_NotFound"] = ("Not found", "Không tìm thấy"),

            // Fields
            ["Field_Id"] = ("ID", "Mã"),
            ["Field_Name"] = ("Name", "Tên"),
            ["Field_Price"] = ("Price", "Giá"),
            ["Field_PreparationTime"] = ("Preparation time (minutes)", "Thời gian chế biến (phút)"),
            ["Field_IsAvailable"] = ("Available", "Đang bán"),
            ["Field_Description"] = ("Description", "Mô tả"),
            ["Field_Category"] = ("Category", "Loại món"),
            ["Field_ImageUrl"] = ("Image URL", "Đường dẫn ảnh"),
            ["Field_IsThumbnail"] = ("Thumbnail", "Ảnh đại diện"),
            ["Field_Status"] = ("Status", "Trạng thái"),
            ["Field_PrepType"] = ("Prep type", "Phân loại chế biến"),
            ["Field_DishCount"] = ("Dishes", "Số món"),
            ["Field_Image"] = ("Image", "Ảnh"),

            // Buttons / actions
            ["Btn_Create"] = ("Create", "Thêm mới"),
            ["Btn_CreateDish"] = ("Add dish", "Thêm món ăn"),
            ["Btn_CreateCategory"] = ("Add category", "Thêm loại món"),
            ["Btn_Save"] = ("Save", "Lưu"),
            ["Btn_Edit"] = ("Edit", "Sửa"),
            ["Btn_Details"] = ("Details", "Chi tiết"),
            ["Btn_Delete"] = ("Delete", "Xóa"),
            ["Btn_ConfirmDelete"] = ("Yes, delete", "Đồng ý xóa"),
            ["Btn_Back"] = ("Back to list", "Quay lại danh sách"),
            ["Btn_Cancel"] = ("Cancel", "Hủy"),
            ["Btn_Search"] = ("Apply", "Áp dụng"),
            ["Btn_Reset"] = ("Reset", "Xóa bộ lọc"),
            ["Btn_AddImage"] = ("Add image", "Thêm ảnh"),
            ["Btn_SetThumbnail"] = ("Set as thumbnail", "Đặt làm ảnh đại diện"),
            ["Btn_DeleteImage"] = ("Delete image", "Xóa ảnh"),

            // Filters
            ["Filter_Search"] = ("Search by name", "Tìm theo tên món"),
            ["Filter_SearchPlaceholder"] = ("Part of dish name...", "Một phần tên món..."),
            ["Filter_AllCategories"] = ("All categories", "Tất cả loại món"),
            ["Filter_AllStatus"] = ("All statuses", "Tất cả trạng thái"),
            ["Filter_MinPrice"] = ("Price from", "Giá từ"),
            ["Filter_MaxPrice"] = ("Price to", "Giá đến"),
            ["Filter_Sort"] = ("Sort by", "Sắp xếp"),
            ["Sort_Default"] = ("Default (name)", "Mặc định (tên)"),
            ["Sort_PriceAsc"] = ("Price: low to high", "Giá tăng dần"),
            ["Sort_PriceDesc"] = ("Price: high to low", "Giá giảm dần"),
            ["Sort_PrepAsc"] = ("Preparation time: shortest first", "Thời gian chế biến tăng dần"),
            ["ResultCount"] = ("{0} dish(es) found", "Tìm thấy {0} món"),

            // Status / classification
            ["Status_Available"] = ("Available", "Đang bán"),
            ["Status_Unavailable"] = ("Unavailable", "Ngừng bán"),
            ["Prep_Quick"] = ("Quick", "Nhanh"),
            ["Prep_Regular"] = ("Regular", "Thông thường"),
            ["Minutes"] = ("{0} min", "{0} phút"),
            ["Yes"] = ("Yes", "Có"),
            ["No"] = ("No", "Không"),

            // Messages
            ["Msg_NoResults"] = ("No dishes match your search.", "Không có món ăn nào phù hợp với điều kiện tìm kiếm."),
            ["Msg_NoCategories"] = ("No categories yet.", "Chưa có loại món ăn nào."),
            ["Msg_NoImages"] = ("This dish has no images yet.", "Món ăn này chưa có ảnh."),
            ["Msg_NoImage"] = ("No image", "Chưa có ảnh"),
            ["Msg_DishCreated"] = ("Dish created successfully.", "Đã thêm món ăn."),
            ["Msg_DishUpdated"] = ("Dish updated successfully.", "Đã cập nhật món ăn."),
            ["Msg_DishDeleted"] = ("Dish deleted.", "Đã xóa món ăn."),
            ["Msg_CategoryCreated"] = ("Category created successfully.", "Đã thêm loại món ăn."),
            ["Msg_CategoryUpdated"] = ("Category updated successfully.", "Đã cập nhật loại món ăn."),
            ["Msg_CategoryDeleted"] = ("Category deleted.", "Đã xóa loại món ăn."),
            ["Msg_CategoryInUse"] = ("Cannot delete category \"{0}\" because {1} dish(es) still use it.", "Không thể xóa loại món \"{0}\" vì còn {1} món ăn đang sử dụng."),
            ["Msg_ImageAdded"] = ("Image added.", "Đã thêm ảnh."),
            ["Msg_ImageDeleted"] = ("Image deleted.", "Đã xóa ảnh."),
            ["Msg_ThumbnailChanged"] = ("Thumbnail changed.", "Đã đổi ảnh đại diện."),
            ["Msg_ConfirmDeleteDish"] = ("Are you sure you want to delete this dish and all its images?", "Bạn có chắc muốn xóa món ăn này cùng toàn bộ ảnh?"),
            ["Msg_ConfirmDeleteCategory"] = ("Are you sure you want to delete this category?", "Bạn có chắc muốn xóa loại món ăn này?"),
            ["Msg_ConfirmDeleteImage"] = ("Delete this image?", "Xóa ảnh này?"),
            ["Msg_FixErrors"] = ("Please correct the errors below.", "Vui lòng sửa các lỗi bên dưới."),

            // Errors
            ["Err_DishNotFound"] = ("Dish with ID {0} does not exist.", "Món ăn có mã {0} không tồn tại."),
            ["Err_CategoryNotFound"] = ("Category with ID {0} does not exist.", "Loại món ăn có mã {0} không tồn tại."),
            ["Err_ImageNotFound"] = ("Image with ID {0} does not exist.", "Ảnh có mã {0} không tồn tại."),
            ["Err_IdMissing"] = ("No ID was provided.", "Không có mã được cung cấp."),
            ["Err_PriceNegative"] = ("Price range cannot be negative.", "Khoảng giá không được âm."),
            ["Err_PriceRange"] = ("Invalid price range: \"Price from\" must not be greater than \"Price to\".", "Khoảng giá không hợp lệ: \"Giá từ\" không được lớn hơn \"Giá đến\"."),
            ["Err_InvalidPriceInput"] = ("Price filter must be a number.", "Giá lọc phải là số."),
            ["Err_DuplicateDishName"] = ("A dish named \"{0}\" already exists in this category.", "Món \"{0}\" đã tồn tại trong loại món này."),
            ["Err_DuplicateCategoryName"] = ("A category named \"{0}\" already exists.", "Loại món \"{0}\" đã tồn tại."),
            ["Err_CategoryInvalid"] = ("The selected category does not exist.", "Loại món đã chọn không tồn tại."),
            ["Err_Generic"] = ("An error occurred while processing your request.", "Đã xảy ra lỗi khi xử lý yêu cầu."),
            ["Err_PageNotFound"] = ("The page you requested does not exist.", "Trang bạn yêu cầu không tồn tại."),

            // Validation (DataAnnotations; {0} = field display name)
            ["Val_Required"] = ("{0} is required.", "{0} không được để trống."),
            ["Val_MaxLength"] = ("{0} must be at most {1} characters.", "{0} tối đa {1} ký tự."),
            ["Val_PricePositive"] = ("{0} must be greater than 0.", "{0} phải lớn hơn 0."),
            ["Val_PrepPositive"] = ("{0} must be greater than 0.", "{0} phải lớn hơn 0."),
            ["Val_CategoryRequired"] = ("Please select a category.", "Vui lòng chọn loại món."),
            ["Val_Url"] = ("{0} must be a valid http/https URL.", "{0} phải là đường dẫn http/https hợp lệ."),
            ["Val_SelectCategory"] = ("-- Select category --", "-- Chọn loại món --"),

            // Model binding messages
            ["Bind_ValueMustBeNumber"] = ("{0} must be a number.", "{0} phải là số."),
            ["Bind_ValueInvalid"] = ("The value '{0}' is not valid for {1}.", "Giá trị '{0}' không hợp lệ cho {1}."),
            ["Bind_ValueRequired"] = ("A value is required.", "Vui lòng nhập giá trị."),
            ["Bind_AttemptedValueInvalid"] = ("The value '{0}' is not valid.", "Giá trị '{0}' không hợp lệ."),
            ["Bind_NonPropertyValueMustBeNumber"] = ("The field must be a number.", "Trường này phải là số."),
        };

        public static bool IsVietnamese =>
            CultureInfo.CurrentUICulture.Name.StartsWith("vi", StringComparison.OrdinalIgnoreCase);

        public static bool TryGet(string key, out string value)
        {
            if (Strings.TryGetValue(key, out var pair))
            {
                value = IsVietnamese ? pair.Vi : pair.En;
                return true;
            }
            value = key;
            return false;
        }

        public static string Get(string key) => TryGet(key, out var v) ? v : key;

        public static string Get(string key, params object[] args) => string.Format(Get(key), args);

        public static IEnumerable<KeyValuePair<string, string>> All() =>
            Strings.Select(kv => new KeyValuePair<string, string>(kv.Key, IsVietnamese ? kv.Value.Vi : kv.Value.En));
    }
}
