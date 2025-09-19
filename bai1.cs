using System;

class MatrixProgram
{
    static void Main(string[] args)
    {
        // Thiết lập Console để hiển thị tiếng Việt
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        while (true)
        {
            Console.Clear(); // Xóa màn hình cho mỗi lần lặp lại menu
            Console.WriteLine("======================================");
            Console.WriteLine("    CHƯƠNG TRÌNH XỬ LÝ MA TRẬN");
            Console.WriteLine("======================================");
            Console.WriteLine("1. Nhập và hiển thị ma trận");
            Console.WriteLine("2. Cộng hai ma trận");
            Console.WriteLine("3. Nhân hai ma trận");
            Console.WriteLine("4. Chuyển vị ma trận");
            Console.WriteLine("5. Tìm giá trị lớn nhất, nhỏ nhất");
            Console.WriteLine("6. Tính định thức (ma trận vuông)");
            Console.WriteLine("7. Kiểm tra ma trận đối xứng (ma trận vuông)");
            Console.WriteLine("0. Thoát chương trình");
            Console.WriteLine("--------------------------------------");
            Console.Write("Mời bạn chọn chức năng: ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập một số.");
                Pause();
                continue;
            }

            try
            {
                switch (choice)
                {
                    case 1:
                        NhapVaHienThi();
                        break;
                    case 2:
                        CongHaiMaTran();
                        break;
                    case 3:
                        NhanHaiMaTran();
                        break;
                    case 4:
                        ChuyenViMaTran();
                        break;
                    case 5:
                        TimMinMax();
                        break;
                    case 6:
                        TinhDinhThuc();
                        break;
                    case 7:
                        KiemTraDoiXung();
                        break;
                    case 0:
                        Console.WriteLine("Đã thoát chương trình.");
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không có. Vui lòng chọn lại.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra lỗi: {ex.Message}");
            }

            Pause();
        }
    }

    // ========== CÁC HÀM TIỆN ÍCH ==========

    /// <summary>
    /// Hàm nhập một ma trận từ người dùng
    /// </summary>
    static double[,] NhapMaTran(string tenMaTran)
    {
        Console.WriteLine($"\n--- NHẬP MA TRẬN {tenMaTran} ---");
        Console.Write("Nhập số dòng: ");
        int rows = int.Parse(Console.ReadLine());
        Console.Write("Nhập số cột: ");
        int cols = int.Parse(Console.ReadLine());

        if (rows <= 0 || cols <= 0)
        {
            throw new ArgumentException("Số dòng và số cột phải lớn hơn 0.");
        }

        double[,] matrix = new double[rows, cols];

        Console.WriteLine("Nhập các phần tử của ma trận:");
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"{tenMaTran}[{i},{j}] = ");
                matrix[i, j] = double.Parse(Console.ReadLine());
            }
        }
        return matrix;
    }

    /// <summary>
    /// Hàm hiển thị ma trận dưới dạng bảng
    /// </summary>
    static void HienThiMaTran(double[,] matrix, string tenMaTran)
    {
        if (matrix == null)
        {
            Console.WriteLine("Ma trận không tồn tại.");
            return;
        }

        Console.WriteLine($"\n--- MA TRẬN {tenMaTran} ---");
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"{matrix[i, j],8:F2}"); // Định dạng số với 8 ký tự, 2 chữ số thập phân
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Dừng màn hình chờ người dùng nhấn phím
    /// </summary>
    static void Pause()
    {
        Console.WriteLine("\nNhấn phím bất kỳ để tiếp tục...");
        Console.ReadKey();
    }

    // ========== CÁC HÀM CHỨC NĂNG ==========

    // Chức năng 1: Nhập và Hiển thị
    static void NhapVaHienThi()
    {
        var A = NhapMaTran("A");
        HienThiMaTran(A, "A");
    }

    // Chức năng 2: Cộng hai ma trận
    static void CongHaiMaTran()
    {
        var A = NhapMaTran("A");
        var B = NhapMaTran("B");

        if (A.GetLength(0) != B.GetLength(0) || A.GetLength(1) != B.GetLength(1))
        {
            Console.WriteLine("\nLỗi: Hai ma trận phải có cùng kích thước để thực hiện phép cộng.");
            return;
        }

        int rows = A.GetLength(0);
        int cols = A.GetLength(1);
        double[,] C = new double[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                C[i, j] = A[i, j] + B[i, j];
            }
        }

        HienThiMaTran(A, "A");
        HienThiMaTran(B, "B");
        HienThiMaTran(C, "Kết quả A + B");
    }

    // Chức năng 3: Nhân hai ma trận
    static void NhanHaiMaTran()
    {
        var A = NhapMaTran("A");
        var B = NhapMaTran("B");

        int rowsA = A.GetLength(0);
        int colsA = A.GetLength(1);
        int rowsB = B.GetLength(0);
        int colsB = B.GetLength(1);

        if (colsA != rowsB)
        {
            Console.WriteLine("\nLỗi: Số cột của ma trận A phải bằng số dòng của ma trận B.");
            return;
        }

        double[,] C = new double[rowsA, colsB];

        for (int i = 0; i < rowsA; i++)
        {
            for (int j = 0; j < colsB; j++)
            {
                C[i, j] = 0;
                for (int k = 0; k < colsA; k++) // Hoặc k < rowsB
                {
                    C[i, j] += A[i, k] * B[k, j];
                }
            }
        }

        HienThiMaTran(A, "A");
        HienThiMaTran(B, "B");
        HienThiMaTran(C, "Kết quả A x B");
    }

    // Chức năng 4: Chuyển vị ma trận
    static void ChuyenViMaTran()
    {
        var A = NhapMaTran("A");
        int rows = A.GetLength(0);
        int cols = A.GetLength(1);

        double[,] AT = new double[cols, rows]; // Kích thước đảo ngược

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                AT[j, i] = A[i, j];
            }
        }

        HienThiMaTran(A, "A");
        HienThiMaTran(AT, "Chuyển vị A^T");
    }

    // Chức năng 5: Tìm giá trị lớn nhất và nhỏ nhất
    static void TimMinMax()
    {
        var A = NhapMaTran("A");
        double min = A[0, 0];
        double max = A[0, 0];

        foreach (var element in A)
        {
            if (element < min)
                min = element;
            if (element > max)
                max = element;
        }

        HienThiMaTran(A, "A");
        Console.WriteLine($"\nGiá trị lớn nhất trong ma trận là: {max}");
        Console.WriteLine($"Giá trị nhỏ nhất trong ma trận là: {min}");
    }

    // ========== CÁC HÀM NÂNG CAO ==========

    // Chức năng 6: Tính định thức
    static void TinhDinhThuc()
    {
        var A = NhapMaTran("A");
        if (A.GetLength(0) != A.GetLength(1))
        {
            Console.WriteLine("\nLỗi: Chỉ có thể tính định thức của ma trận vuông.");
            return;
        }
        
        double det = Determinant(A);
        HienThiMaTran(A, "A");
        Console.WriteLine($"\nĐịnh thức của ma trận A là: {det:F2}");
    }

    /// <summary>
    /// Hàm đệ quy tính định thức bằng phương pháp khai triển Laplace
    /// </summary>
    static double Determinant(double[,] matrix)
    {
        int n = matrix.GetLength(0);
        if (n == 1)
        {
            return matrix[0, 0];
        }
        if (n == 2)
        {
            return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        }

        double det = 0;
        for (int j = 0; j < n; j++)
        {
            det += Math.Pow(-1, j) * matrix[0, j] * Determinant(CreateSubMatrix(matrix, 0, j));
        }
        return det;
    }

    /// <summary>
    /// Tạo ma trận con (minor) bằng cách loại bỏ một hàng và một cột
    /// </summary>
    static double[,] CreateSubMatrix(double[,] matrix, int rowToRemove, int colToRemove)
    {
        int n = matrix.GetLength(0);
        double[,] subMatrix = new double[n - 1, n - 1];
        int r = -1;
        for (int i = 0; i < n; i++)
        {
            if (i == rowToRemove) continue;
            r++;
            int c = -1;
            for (int j = 0; j < n; j++)
            {
                if (j == colToRemove) continue;
                c++;
                subMatrix[r, c] = matrix[i, j];
            }
        }
        return subMatrix;
    }

    // Chức năng 7: Kiểm tra ma trận đối xứng
    static void KiemTraDoiXung()
    {
        var A = NhapMaTran("A");
        
        if (A.GetLength(0) != A.GetLength(1))
        {
            HienThiMaTran(A, "A");
            Console.WriteLine("\nMa trận không phải ma trận vuông nên không phải là ma trận đối xứng.");
            return;
        }

        int n = A.GetLength(0);
        bool isSymmetric = true;
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++) // Chỉ cần kiểm tra nửa trên (hoặc dưới)
            {
                if (A[i, j] != A[j, i])
                {
                    isSymmetric = false;
                    break;
                }
            }
            if (!isSymmetric) break;
        }

        HienThiMaTran(A, "A");
        if (isSymmetric)
        {
            Console.WriteLine("\nĐây là ma trận đối xứng.");
        }
        else
        {
            Console.WriteLine("\nĐây không phải là ma trận đối xứng.");
        }
    }
}