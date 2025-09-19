using System;
using System.Linq; 

/// <summary>
/// Lớp ArrayProcessor chứa các phương thức để xử lý một mảng số nguyên,
/// bao gồm nhập, hiển thị, sắp xếp và tìm kiếm.
/// </summary>
public class ArrayProcessor
{
    // Thuộc tính: một mảng số nguyên, private để đảm bảo tính đóng gói
    private int[] array;

    // --- CÁC PHƯƠNG THỨC ---

    /// <summary>
    /// Phương thức cho phép nhập mảng từ bàn phím.
    /// </summary>
    public void Input()
    {
        Console.Write("Nhập số lượng phần tử của mảng: ");
        int n = Convert.ToInt32(Console.ReadLine());
        array = new int[n];

        Console.WriteLine("Nhập các phần tử của mảng:");
        for (int i = 0; i < n; i++)
        {
            Console.Write($"Phần tử thứ {i + 1}: ");
            array[i] = Convert.ToInt32(Console.ReadLine());
        }
    }

    /// <summary>
    /// Phương thức hiển thị các phần tử của mảng.
    /// </summary>
    public void Display()
    {
        if (array == null)
        {
            Console.WriteLine("Mảng chưa được khởi tạo.");
            return;
        }
        Console.WriteLine($"[{string.Join(", ", array)}]");
    }

    /// <summary>
    /// Sắp xếp mảng tăng dần bằng thuật toán Bubble Sort.
    /// </summary>
    public void BubbleSort()
    {
        if (array == null || array.Length < 2)
        {
            return; // Không cần sắp xếp
        }
        int n = array.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    // Hoán đổi array[j] và array[j+1]
                    // Cách 1: Dùng biến tạm
                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;

                    // Cách 2: Dùng Tuple Swap trong C# 7.0 trở lên
                    // (array[j], array[j + 1]) = (array[j + 1], array[j]);
                }
            }
        }
    }

    /// <summary>
    /// Phương thức phân hoạch (partition) cho QuickSort
    /// </summary>
    private int Partition(int left, int right)
    {
        int pivot = array[right]; // Chọn pivot là phần tử cuối
        int i = (left - 1); // Chỉ số của phần tử nhỏ hơn pivot

        for (int j = left; j < right; j++)
        {
            // Nếu phần tử hiện tại nhỏ hơn hoặc bằng pivot
            if (array[j] <= pivot)
            {
                i++;
                // Hoán đổi array[i] và array[j]
                (array[i], array[j]) = (array[j], array[i]);
            }
        }

        // Đưa pivot về đúng vị trí
        (array[i + 1], array[right]) = (array[right], array[i + 1]);
        return i + 1;
    }

    /// <summary>
    /// Sắp xếp mảng tăng dần bằng thuật toán Quick Sort (Sắp xếp nhanh).
    /// </summary>
    /// <param name="left">Chỉ số bắt đầu.</param>
    /// <param name="right">Chỉ số kết thúc.</param>
    public void QuickSort(int left, int right)
    {
        if (array == null || array.Length < 2)
        {
            return;
        }
        if (left < right)
        {
            // pi là chỉ số nơi pivot đã đứng đúng vị trí
            int pi = Partition(left, right);

            // Đệ quy sắp xếp các phần tử trước và sau pivot
            QuickSort(left, pi - 1);
            QuickSort(pi + 1, right);
        }
    }

    /// <summary>
    /// Tìm kiếm tuyến tính một giá trị trong mảng.
    /// </summary>
    /// <param name="key">Giá trị cần tìm.</param>
    /// <returns>Vị trí đầu tiên của key trong mảng, hoặc -1 nếu không tìm thấy.</returns>
    public int LinearSearch(int key)
    {
        if (array == null)
        {
            return -1;
        }
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == key)
            {
                return i; // Trả về vị trí đầu tiên tìm thấy
            }
        }
        return -1; // Không tìm thấy
    }

    /// <summary>
    /// Tìm kiếm nhị phân một giá trị trong mảng (mảng phải được sắp xếp trước).
    /// </summary>
    /// <param name="key">Giá trị cần tìm.</param>
    /// <returns>Vị trí của key trong mảng, hoặc -1 nếu không tìm thấy.</returns>
    public int BinarySearch(int key)
    {
        if (array == null)
        {
            return -1;
        }
        int left = 0, right = array.Length - 1;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            // Nếu tìm thấy key tại mid
            if (array[mid] == key)
            {
                return mid;
            }

            // Nếu key lớn hơn, bỏ qua nửa bên trái
            if (array[mid] < key)
            {
                left = mid + 1;
            }
            // Nếu key nhỏ hơn, bỏ qua nửa bên phải
            else
            {
                right = mid - 1;
            }
        }
        return -1; // Không tìm thấy
    }
    
    // Phương thức getter để lấy bản sao của mảng
    public int[] GetArray()
    {
        // Trả về một bản sao để mảng gốc không bị thay đổi từ bên ngoài
        return array.ToArray();
    }
    
    // Phương thức setter để thiết lập mảng từ một mảng khác
    public void SetArray(int[] newArray)
    {
        // Tạo bản sao để đối tượng này không chia sẻ tham chiếu với mảng bên ngoài
        this.array = newArray.ToArray();
    }
}